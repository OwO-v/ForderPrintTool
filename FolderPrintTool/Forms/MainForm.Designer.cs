namespace ForderPrintTool.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_run = new System.Windows.Forms.Button();
            this.treeFiles = new System.Windows.Forms.TreeView();
            this.fb_dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lb_path = new System.Windows.Forms.Label();
            this.btn_select = new System.Windows.Forms.Button();
            this.lb_exStat = new System.Windows.Forms.Label();
            this.btn_stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_next = new System.Windows.Forms.Button();
            this.lb_exDir = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lb_stat = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb_dir = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb_file = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printerSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.alwaysTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_run
            // 
            this.btn_run.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_run.Location = new System.Drawing.Point(338, -1);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 24);
            this.btn_run.TabIndex = 0;
            this.btn_run.Text = "Start";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeFiles
            // 
            this.treeFiles.CheckBoxes = true;
            this.treeFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFiles.Location = new System.Drawing.Point(18, 33);
            this.treeFiles.Name = "treeFiles";
            this.treeFiles.Size = new System.Drawing.Size(487, 217);
            this.treeFiles.TabIndex = 1;
            // 
            // lb_path
            // 
            this.lb_path.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lb_path.AutoSize = true;
            this.lb_path.Location = new System.Drawing.Point(126, 8);
            this.lb_path.Name = "lb_path";
            this.lb_path.Size = new System.Drawing.Size(45, 12);
            this.lb_path.TabIndex = 2;
            this.lb_path.Text = "(None)";
            // 
            // btn_select
            // 
            this.btn_select.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_select.Location = new System.Drawing.Point(3, 3);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(117, 23);
            this.btn_select.TabIndex = 3;
            this.btn_select.Text = "Select Folder...";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // lb_exStat
            // 
            this.lb_exStat.AutoEllipsis = true;
            this.lb_exStat.AutoSize = true;
            this.lb_exStat.Location = new System.Drawing.Point(3, 7);
            this.lb_exStat.Name = "lb_exStat";
            this.lb_exStat.Size = new System.Drawing.Size(48, 12);
            this.lb_exStat.TabIndex = 4;
            this.lb_exStat.Text = "Status: ";
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_stop.Location = new System.Drawing.Point(418, -1);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 24);
            this.btn_stop.TabIndex = 5;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(302, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "* Double clicked: Print selected file";
            // 
            // btn_next
            // 
            this.btn_next.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_next.Enabled = false;
            this.btn_next.Location = new System.Drawing.Point(234, -1);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(98, 24);
            this.btn_next.TabIndex = 7;
            this.btn_next.Text = "Next";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // lb_exDir
            // 
            this.lb_exDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_exDir.AutoSize = true;
            this.lb_exDir.Location = new System.Drawing.Point(3, 6);
            this.lb_exDir.Name = "lb_exDir";
            this.lb_exDir.Size = new System.Drawing.Size(87, 12);
            this.lb_exDir.TabIndex = 8;
            this.lb_exDir.Text = "Now working: ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeFiles, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(30, 25, 30, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(523, 361);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lb_stat);
            this.panel4.Controls.Add(this.lb_exStat);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(15, 271);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(493, 25);
            this.panel4.TabIndex = 8;
            // 
            // lb_stat
            // 
            this.lb_stat.AutoSize = true;
            this.lb_stat.Location = new System.Drawing.Point(57, 7);
            this.lb_stat.Name = "lb_stat";
            this.lb_stat.Size = new System.Drawing.Size(50, 12);
            this.lb_stat.TabIndex = 5;
            this.lb_stat.Text = "(Status)";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lb_dir);
            this.panel3.Controls.Add(this.lb_exDir);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(15, 296);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(493, 25);
            this.panel3.TabIndex = 8;
            // 
            // lb_dir
            // 
            this.lb_dir.AutoSize = true;
            this.lb_dir.Location = new System.Drawing.Point(87, 6);
            this.lb_dir.Name = "lb_dir";
            this.lb_dir.Size = new System.Drawing.Size(65, 12);
            this.lb_dir.TabIndex = 9;
            this.lb_dir.Text = "(Directory)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_select);
            this.panel1.Controls.Add(this.lb_path);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(15, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 30);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lb_file);
            this.panel2.Controls.Add(this.btn_stop);
            this.panel2.Controls.Add(this.btn_run);
            this.panel2.Controls.Add(this.btn_next);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(15, 321);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(493, 25);
            this.panel2.TabIndex = 11;
            // 
            // lb_file
            // 
            this.lb_file.AutoSize = true;
            this.lb_file.Location = new System.Drawing.Point(3, 5);
            this.lb_file.Name = "lb_file";
            this.lb_file.Size = new System.Drawing.Size(35, 12);
            this.lb_file.TabIndex = 10;
            this.lb_file.Text = "(File)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(523, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printerSettingToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.alwaysTopToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // printerSettingToolStripMenuItem
            // 
            this.printerSettingToolStripMenuItem.Name = "printerSettingToolStripMenuItem";
            this.printerSettingToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.printerSettingToolStripMenuItem.Text = "Set Default Printer (System)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.toolStripMenuItem1.Text = "Open Print Settings";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(224, 22);
            this.toolStripMenuItem2.Text = "Open Print Queue";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // alwaysTopToolStripMenuItem
            // 
            this.alwaysTopToolStripMenuItem.Checked = true;
            this.alwaysTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysTopToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alwaysTopToolStripMenuItem.Name = "alwaysTopToolStripMenuItem";
            this.alwaysTopToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.alwaysTopToolStripMenuItem.Text = "Always Top";
            this.alwaysTopToolStripMenuItem.CheckedChanged += new System.EventHandler(this.alwaysTopToolStripMenuItem_CheckedChanged);
            // 
            // 설정ToolStripMenuItem
            // 
            this.설정ToolStripMenuItem.Name = "설정ToolStripMenuItem";
            this.설정ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(523, 385);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Folder Print Manager";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.TreeView treeFiles;
        private System.Windows.Forms.FolderBrowserDialog fb_dialog;
        private System.Windows.Forms.Label lb_path;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Label lb_exStat;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Label lb_exDir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lb_stat;
        private System.Windows.Forms.Label lb_dir;
        private System.Windows.Forms.Label lb_file;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printerSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem alwaysTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}

