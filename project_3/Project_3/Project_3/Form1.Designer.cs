namespace Project_3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMaster = new Panel();
            pnlMiddle = new Panel();
            splitContFiles = new SplitContainer();
            tvDir = new TreeView();
            lstFileInfo = new ListBox();
            rtbFileText = new RichTextBox();
            pnlTop = new Panel();
            btnSearch = new Button();
            txtPath = new TextBox();
            btnDirDown = new Button();
            btnDirUp = new Button();
            pnlBottom = new Panel();
            btnSave = new Button();
            btnEdit = new Button();
            btnReadFile = new Button();
            pnlMaster.SuspendLayout();
            pnlMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContFiles).BeginInit();
            splitContFiles.Panel1.SuspendLayout();
            splitContFiles.Panel2.SuspendLayout();
            splitContFiles.SuspendLayout();
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMaster
            // 
            pnlMaster.Controls.Add(pnlMiddle);
            pnlMaster.Controls.Add(pnlTop);
            pnlMaster.Controls.Add(pnlBottom);
            pnlMaster.Dock = DockStyle.Fill;
            pnlMaster.Location = new Point(0, 0);
            pnlMaster.Name = "pnlMaster";
            pnlMaster.Size = new Size(800, 450);
            pnlMaster.TabIndex = 0;
            // 
            // pnlMiddle
            // 
            pnlMiddle.Controls.Add(splitContFiles);
            pnlMiddle.Dock = DockStyle.Fill;
            pnlMiddle.Location = new Point(0, 49);
            pnlMiddle.Name = "pnlMiddle";
            pnlMiddle.Padding = new Padding(10);
            pnlMiddle.Size = new Size(800, 350);
            pnlMiddle.TabIndex = 1;
            // 
            // splitContFiles
            // 
            splitContFiles.Dock = DockStyle.Fill;
            splitContFiles.Location = new Point(10, 10);
            splitContFiles.Name = "splitContFiles";
            // 
            // splitContFiles.Panel1
            // 
            splitContFiles.Panel1.BackColor = Color.White;
            splitContFiles.Panel1.Controls.Add(tvDir);
            splitContFiles.Panel1.Margin = new Padding(3, 0, 0, 0);
            // 
            // splitContFiles.Panel2
            // 
            splitContFiles.Panel2.BackColor = Color.White;
            splitContFiles.Panel2.Controls.Add(lstFileInfo);
            splitContFiles.Panel2.Controls.Add(rtbFileText);
            splitContFiles.Panel2.Margin = new Padding(0, 0, 3, 0);
            splitContFiles.Size = new Size(780, 330);
            splitContFiles.SplitterDistance = 331;
            splitContFiles.TabIndex = 0;
            // 
            // tvDir
            // 
            tvDir.Dock = DockStyle.Fill;
            tvDir.Location = new Point(0, 0);
            tvDir.Name = "tvDir";
            tvDir.Size = new Size(331, 330);
            tvDir.TabIndex = 0;
            tvDir.AfterSelect += tvDir_AfterSelect;
            // 
            // lstFileInfo
            // 
            lstFileInfo.BackColor = Color.White;
            lstFileInfo.Dock = DockStyle.Fill;
            lstFileInfo.ForeColor = Color.Black;
            lstFileInfo.FormattingEnabled = true;
            lstFileInfo.ItemHeight = 15;
            lstFileInfo.Location = new Point(0, 0);
            lstFileInfo.Margin = new Padding(0);
            lstFileInfo.Name = "lstFileInfo";
            lstFileInfo.Size = new Size(445, 330);
            lstFileInfo.TabIndex = 0;
            // 
            // rtbFileText
            // 
            rtbFileText.BackColor = Color.White;
            rtbFileText.Dock = DockStyle.Fill;
            rtbFileText.Location = new Point(0, 0);
            rtbFileText.Name = "rtbFileText";
            rtbFileText.ReadOnly = true;
            rtbFileText.Size = new Size(445, 330);
            rtbFileText.TabIndex = 0;
            rtbFileText.Text = "";
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(txtPath);
            pnlTop.Controls.Add(btnDirDown);
            pnlTop.Controls.Add(btnDirUp);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(800, 49);
            pnlTop.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(631, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(136, 25);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            txtPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPath.Location = new Point(361, 11);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(264, 25);
            txtPath.TabIndex = 1;
            // 
            // btnDirDown
            // 
            btnDirDown.Location = new Point(153, 7);
            btnDirDown.Name = "btnDirDown";
            btnDirDown.Size = new Size(46, 33);
            btnDirDown.TabIndex = 5;
            btnDirDown.Text = "Down";
            btnDirDown.UseVisualStyleBackColor = true;
            // 
            // btnDirUp
            // 
            btnDirUp.Location = new Point(90, 7);
            btnDirUp.Name = "btnDirUp";
            btnDirUp.Size = new Size(46, 33);
            btnDirUp.TabIndex = 4;
            btnDirUp.Text = "Up";
            btnDirUp.UseVisualStyleBackColor = true;
            btnDirUp.Click += btnDirUp_Click;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Controls.Add(btnEdit);
            pnlBottom.Controls.Add(btnReadFile);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 399);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(800, 51);
            pnlBottom.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(702, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 29);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Visible = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(610, 6);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(86, 29);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Visible = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnReadFile
            // 
            btnReadFile.Location = new Point(90, 11);
            btnReadFile.Name = "btnReadFile";
            btnReadFile.Size = new Size(136, 28);
            btnReadFile.TabIndex = 3;
            btnReadFile.Text = "View Contents";
            btnReadFile.UseVisualStyleBackColor = true;
            btnReadFile.Click += btnReadFile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlMaster);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "File System Implementation";
            pnlMaster.ResumeLayout(false);
            pnlMiddle.ResumeLayout(false);
            splitContFiles.Panel1.ResumeLayout(false);
            splitContFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContFiles).EndInit();
            splitContFiles.ResumeLayout(false);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMaster;
        private SplitContainer splitContFiles;
        private Button btnReadFile;
        private Button btnSearch;
        private TextBox txtPath;
        private System.Windows.Forms.TreeView tvDir;
        private ListBox lstFileInfo;
        private Panel pnlTop;
        private Panel pnlBottom;
        private Panel pnlMiddle;
        private RichTextBox rtbFileText;
        private Button btnDirDown;
        private Button btnDirUp;
        private Button btnSave;
        private Button btnEdit;
    }
}
