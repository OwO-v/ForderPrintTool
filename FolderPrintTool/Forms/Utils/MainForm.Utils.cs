using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForderPrintTool.Forms
{
    partial class MainForm
    {
        
        internal static bool _kor = true;
        private CancellationTokenSource _cts = null;

        private bool _isRunning = false;
        private bool _suppressCheckEvents = false;

        private Queue<string> _printQueue = new Queue<string>();

        private List<string> _folderKeys = null;
        private int _currentFolderIndex = 0;
        private Dictionary<string, List<string>> _folderToFiles = null;
        private TaskCompletionSource<bool> _waitForNextFolder = null;
        private List<ToolStripMenuItem> _menuItemPrintAll = null;

        #region Win32 API Imports
        
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string printerName);
        
        #endregion

        #region Common Methods
        private void SS(string msg)
        {
            if (!_isRunning)
                return;

            Invoker(lb_stat, () => lb_stat.Text = msg);
        }

        private void SP(string path)
        {
            var fileName = Path.GetFileName(path);
            var dirName = Path.GetDirectoryName(path) ?? string.Empty;

            Invoker(lb_file, () => lb_file.Text = fileName);
            Invoker(lb_dir, () => lb_dir.Text = dirName);
        }

        /// <summary>
        /// Button Enable/Disable Helper
        /// </summary>
        /// <param name="enable">isRunning -> false</param>
        private void OnOffButtons(bool enable)
        {
            _isRunning = !enable;

            if (_isRunning)
            {
                Invoker(btn_run, () => btn_run.Enabled = false);
                Invoker(treeFiles, () => treeFiles.Enabled = false);
                Invoker(btn_next, () => btn_next.Enabled = false);
                Invoker(btn_stop, () => btn_stop.Enabled = true);
            }
            else
            {
                Invoker(btn_run, () => btn_run.Enabled = true);
                Invoker(treeFiles, () => treeFiles.Enabled = true);
                Invoker(btn_next, () => btn_next.Enabled = false);
                Invoker(btn_stop, () => btn_stop.Enabled = false);
            }
        }

        private void Invoker(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(() => action()));
            }
            else
            {
                action();
            }
        }

        private string GetPrintCommandFromRegistry(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension) || !extension.StartsWith("."))
            {
                return null;
            }
            SS((_kor ? "확장자 분석 중: " : "Analyzing extension: ") + extension);
            try
            {
                using (var extKey = Registry.ClassesRoot.OpenSubKey(extension))
                {
                    string progId = extKey?.GetValue("") as string;
                    if (string.IsNullOrWhiteSpace(progId))
                        return null;

                    string cmdKeyPath = $@"{progId}\shell\print\command";
                    using (var cmdKey = Registry.ClassesRoot.OpenSubKey(cmdKeyPath))
                    {
                        return cmdKey?.GetValue("") as string;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private string ReplacePlaceholders(string command, string filePath)
        {
            SS((_kor ? "명령어 분석 중: " : "Parsing print command: ") + command);
            string quotedPath = $"\"{filePath}\"";
            return Regex.Replace(command, @"(?i)([""']?)%1([""']?)", quotedPath);
        }

        private void PrinterCollection()
        {
            printerSettingToolStripMenuItem.DropDownItems.Clear();

            try
            {
                var printers = PrinterSettings.InstalledPrinters.Cast<string>().ToList();
            
                foreach(var printer in printers)
                {
                    var item = new ToolStripMenuItem(printer)
                    {
                        CheckOnClick = true,
                        Checked = printer == new PrinterSettings().PrinterName,
                        Tag = printer,
                    };
                    item.Click += (s, e) =>
                    {
                        var selectedPrinter = (s as ToolStripMenuItem).Tag as string;
                        if (selectedPrinter != null)
                        {
                            try
                            {
                                SetDefaultPrinter(printer);
                                Console.WriteLine(Marshal.GetLastWin32Error());
                                PrinterCollection();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    };
                    printerSettingToolStripMenuItem.DropDownItems.Add(item);
                }

                
            }
            finally
            {
                if (printerSettingToolStripMenuItem.DropDownItems.Count > 0)
                    printerSettingToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                
                var refreshItem = new ToolStripMenuItem("새로고침");
                refreshItem.Click += (s, e) =>
                {
                    PrinterCollection();
                };

                printerSettingToolStripMenuItem.DropDownItems.Add(refreshItem);
            }

        }

        #endregion

        #region Workflow Methods

        private async Task TryAdd(string filePath, CancellationToken token)
        {
            var watcher = new Function.PrintJobWatcher();
            var before = watcher.GetCurrentJobNames();

            await ExecutePrint(filePath);

            bool added = await Task.Run(() =>
            {
                return watcher.WaitForNewPrintJob(15000, before);
            });

            if (added)
            {
                SS(_kor ? "대기열 추가 완료" : "Print job added");
                await PrintNextFile(token);
            }
            else
            {
                var msgResult = MessageBox.Show(new Form() { TopMost = true }, (_kor ?
                    "프린터가 응답하지 않거나 대기열 상태를 확인할 수 없습니다.\n" :
                    "The printer did not respond or the print queue could not be confirm.\n") +
                    (_kor ? "다시 시도하려면 확인, 다음 파일을 출력하려면 취소를 선택하세요." :
                    "Click Yes to retry, or No to skip this file.") +
                    "\n\nFile: " + filePath,
                    "Printer Error", MessageBoxButtons.YesNo);

                if (msgResult == DialogResult.Yes)
                    await TryAdd(filePath, token);

                SS(_kor ? "다음 작업을 진행합니다." : "Continuing to the next job.");
                await PrintNextFile(token);
            }
        }

        private async Task ExecutePrint(string filePath)
        {
            SP(filePath);
            await Task.Delay(2000);

            if (!File.Exists(filePath))
            {
                var msgResult = MessageBox.Show(new Form() { TopMost = true }, (_kor ?
                    "파일을 인식할 수 없습니다.\n" : "File not found or cannot be recognized.\n") +
                    (_kor ? "다음 파일을 진행하려면 예, 중단하려면 취소를 눌러주세요" :
                    "Press Yes to proceed to the next file, or Cancel to No."),
                    "\n\nFile: " + filePath +
                    "Warning", MessageBoxButtons.YesNo);

                if (msgResult == DialogResult.Yes)
                    return;

                _cts?.Cancel();
                return;
            }

            string ext = Path.GetExtension(filePath)?.ToLower();
            if (string.IsNullOrEmpty(ext))
            {
                SS(_kor ? "오류: 파일 확장자를 인식할 수 없습니다\n" : "Error: File extension not recognized\n");
                var msgResult = MessageBox.Show(new Form() { TopMost = true }, (_kor ? 
                    "오류: 파일 확장자를 인식할 수 없습니다" : "File extension not recognized.") +
                    (_kor ? "다음 파일을 진행하려면 예, 중단하려면 취소를 눌러주세요" :
                    "Press Yes to proceed to the next file, or Cancel to No."),
                    "\n\nFile: " + filePath +
                    "Warning", MessageBoxButtons.YesNo);

                if (msgResult == DialogResult.Yes)
                    return;

                _cts?.Cancel();
                return;
            }


            if (ext.Equals(".png") || ext.Equals(".jpeg") || ext.Equals(".jpg") || ext.Equals(".gif"))
            {
                SS(_kor ? "이미지 인쇄 중(최대 15s) " : "Printing image(Up to 15s) ");
                SP(filePath);
                var printer = new Function.ImagePrinter();
                printer.PrintImage(filePath);

                return;
            }

            string command = GetPrintCommandFromRegistry(ext);

            if (string.IsNullOrEmpty(command))
            {
                SS(_kor ? $"오류: 해당 확장자({ext})에 대한 인쇄 명령을 찾을 수 없습니다." : $"Error: No print command found for extension ({ext}).");
                var msgResult = MessageBox.Show(new Form() { TopMost = true }, (_kor ? 
                    $"해당 확장자({ext})에 대한 인쇄 명령을 찾을 수 없습니다.\n" :
                    $"No print command found for extension ({ext}).\n") +
                    (_kor ? "다음 파일을 진행하려면 예, 중단하려면 취소를 눌러주세요" : 
                    "Press Yes to proceed to the next file, or Cancel to No."),
                    "\n\nFile:" + filePath +
                    "Warning", MessageBoxButtons.YesNo);

                if (msgResult == DialogResult.Yes)
                    return;

                _cts?.Cancel();
                return;
            }

            string parsedCommand = ReplacePlaceholders(command, filePath);
            SS((_kor ? "인쇄 명령 실행 중(최대 15s): " : "Executing print command(Up to 15s): ") + parsedCommand);

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C \"{parsedCommand}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                SS(_kor ? "인쇄 중 오류" : "Print error");
                var msgResult = MessageBox.Show(new Form() { TopMost = true }, (_kor ? 
                    "인쇄 중 오류가 발생했습니다.\n" : "An error occurred during printing.\n") +
                    (_kor ? "다음 파일을 진행하려면 예, 중단하려면 취소를 눌러주세요" :
                    "Press Yes to proceed to the next file, or Cancel to No."),
                    "\n\nFile: " + filePath + "\n" +
                    "Error: " + ex.Message +
                    "Error", MessageBoxButtons.YesNo);

                if (msgResult == DialogResult.Yes)
                    return;

                _cts?.Cancel();
                return;
            }
        }


        private async Task LoadNextFolder(CancellationToken token)
        {
            string currentFolder = _folderKeys[_currentFolderIndex];
            SS(_kor ? "신규 폴더 인쇄 시작" : "Starting print for new folder");
            SP(currentFolder);
            HighlightCurrentFolder(currentFolder);

            var files = _folderToFiles[currentFolder];
            _printQueue = new Queue<string>(files);

            await PrintNextFile(token);
            _currentFolderIndex++;
        }

        private async Task PrintNextFile(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            if (_printQueue.Count == 0)
            {
                SS(_kor ? "작업 완료. 'Next' 버튼을 누르면 다음 폴더를 시작합니다." : 
                    "Current folder done. Press 'Next' button to start next folder");
                Invoker(btn_next, () => btn_next.Enabled = true);

                return;
            }
            string filePath = _printQueue.Dequeue();

            await TryAdd(filePath, token);

        }

        private async Task ProcessAllFoldersAsync(CancellationToken token)
        {
            while (_currentFolderIndex < _folderKeys.Count)
            {
                token.ThrowIfCancellationRequested();

                var folder = _folderKeys[_currentFolderIndex];
                var files = _folderToFiles[folder];
                var fileQueue = new Queue<string>(files);

                await LoadNextFolder(token);

                _waitForNextFolder = new TaskCompletionSource<bool>();
                try
                {
                    await _waitForNextFolder.Task;
                }
                catch (OperationCanceledException)
                {
                    SS(_kor ? "인쇄 작업이 중단되었습니다." : "Print job has been cancelled.");

                    return;
                }
            }
        }

        #endregion

        #region Folder Processing
        public Dictionary<string, List<string>> GetGroupedCheckedFiles()
        {
            SS(_kor ? "파일 그룹화 중..." : "Grouping files...");
            var files = GetCheckedFiles();
            return files
                .GroupBy(f => Path.GetDirectoryName(f))
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.OrderBy(f => Path.GetFileName(f)).ToList());
        }
        public void StartPrintingByFolder(Dictionary<string, List<string>> groupedFiles)
        {
            SS(_kor ? "폴더 분석 중" : "Analyzing folders");
            _folderToFiles = groupedFiles;
            _folderKeys = groupedFiles.Keys.ToList();
            _currentFolderIndex = 0;
            _printQueue.Clear();
        }
        #endregion

        #region TreeView Helpers

        private void HighlightCurrentFolder(string folderPath)
        {
            ClearTreeSelection(treeFiles.Nodes);

            TreeNode nodeToHighlight = FindNodeByPath(treeFiles.Nodes, folderPath);
            if (nodeToHighlight != null)
            {
                treeFiles.SelectedNode = nodeToHighlight;
                nodeToHighlight.EnsureVisible();
            }
        }
        private TreeNode FindNodeByPath(TreeNodeCollection nodes, string targetPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag?.ToString() == targetPath)
                    return node;

                TreeNode found = FindNodeByPath(node.Nodes, targetPath);
                if (found != null)
                    return found;
            }
            return null;
        }
        private void ClearTreeSelection(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (treeFiles.SelectedNode == node)
                {
                    treeFiles.SelectedNode = null;
                }

                ClearTreeSelection(node.Nodes);
            }
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo dirInfo)
        {
            TreeNode node = new TreeNode(dirInfo.Name)
            {
                Tag = dirInfo.FullName
            };

            foreach (var dir in dirInfo.GetDirectories().OrderBy(d => d.Name))
            {
                node.Nodes.Add(CreateDirectoryNode(dir));
            }

            foreach (var file in dirInfo.GetFiles().OrderBy(f => f.Name))
            {
                node.Nodes.Add(new TreeNode(file.Name) { Tag = file.FullName });
            }

            return node;
        }

        private async void TreeFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string fullPath = e.Node.Tag?.ToString();
            if (File.Exists(fullPath))
            {
                if (MessageBox.Show(new Form() { TopMost = true }, (_kor ? 
                    "파일을 출력하시겠습니까?\n" : "Do you want to print the selected file?\n") 
                    + fullPath, (_kor ? "프린트 출력" : "Print file"), 
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await ExecutePrint(fullPath);
                }
            }
        }

        private void TreeFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_suppressCheckEvents) return;

            try
            {
                _suppressCheckEvents = true;

                CheckAllChildNodes(e.Node, e.Node.Checked);

                UpdateParentNodeCheckState(e.Node);
            }
            finally
            {
                _suppressCheckEvents = false;
            }
        }

        private void CheckAllChildNodes(TreeNode node, bool isChecked)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
                CheckAllChildNodes(child, isChecked);
            }
        }

        private void UpdateParentNodeCheckState(TreeNode node)
        {
            TreeNode parent = node.Parent;
            while (parent != null)
            {
                bool allChecked = parent.Nodes.Cast<TreeNode>().All(n => n.Checked);
                bool noneChecked = parent.Nodes.Cast<TreeNode>().All(n => !n.Checked);

                parent.Checked = allChecked;

                parent = parent.Parent;
            }
        }

        public List<string> GetCheckedFiles()
        {
            var result = new List<string>();
            TraverseCheckedNodes(treeFiles.Nodes, result);
            return result;
        }

        private void TraverseCheckedNodes(TreeNodeCollection nodes, List<string> result)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked && File.Exists(node.Tag?.ToString()))
                {
                    result.Add(node.Tag.ToString());
                }

                if (node.Nodes.Count > 0)
                {
                    TraverseCheckedNodes(node.Nodes, result);
                }
            }
        }
        #endregion
    }
}
