using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForderPrintTool.Forms
{
    public partial class MainForm : Form
    {
        private string _lastSelectedPath = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            treeFiles.NodeMouseDoubleClick += TreeFiles_NodeMouseDoubleClick;
            treeFiles.AfterCheck += TreeFiles_AfterCheck;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
                SS(_kor ? "실행 중..." : "Running...");
                _cts = new CancellationTokenSource();

                OnOffButtons(false);

                _printQueue?.Clear();
                _folderToFiles?.Clear();
                _folderKeys?.Clear();
                _currentFolderIndex = 0;
                _waitForNextFolder = null;

                StartPrintingByFolder(GetGroupedCheckedFiles());
                await ProcessAllFoldersAsync(_cts.Token);
                SS(_kor ? "작업 완료" : "Task completed");
            }
            catch (OperationCanceledException)
            {
                SS(_kor ? "사용자 또는 오류로 인한 중단" : "Cancelled by user or error");
            }
            catch (Exception ex)
            {
                SS((_kor ? "예외 발생: " : "Exception occurred: ") + ex.Message);
            }
            finally
            {
                OnOffButtons(true);
            }
        }
        private void btn_select_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (!string.IsNullOrEmpty(_lastSelectedPath) && Directory.Exists(_lastSelectedPath))
                {
                    fbd.SelectedPath = _lastSelectedPath;
                }
                else
                {
                    fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    treeFiles.Nodes.Clear();
                    DirectoryInfo rootDir = new DirectoryInfo(fbd.SelectedPath);
                    TreeNode rootNode = CreateDirectoryNode(rootDir);
                    treeFiles.Nodes.Add(rootNode);
                    rootNode.Expand();
                    lb_path.Text = fbd.SelectedPath;
                }
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            _waitForNextFolder?.TrySetCanceled();

            SS(_kor ? "인쇄 작업이 중단되었습니다." : "Print job has been cancelled.");

            OnOffButtons(true);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            SS(_kor ? "다음 폴더 인쇄 중..." : "Printing next folder...");
            Invoker(btn_next, () => btn_next.Enabled = false);

            _waitForNextFolder?.TrySetResult(true);
        }

        private void alwaysTopToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = alwaysTopToolStripMenuItem.Checked;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            printerSettingToolStripMenuItem.DropDownItems.Clear();
            PrinterCollection();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "rundll32.exe",
                Arguments = $"printui.dll,PrintUIEntry /e /n \"{new PrinterSettings().PrinterName}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(psi);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "rundll32.exe",
                Arguments = $"printui.dll,PrintUIEntry /o /n \"{new PrinterSettings().PrinterName}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(psi);
        }

    }
}

