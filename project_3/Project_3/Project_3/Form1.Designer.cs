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
            splitContainer1 = new SplitContainer();
            tvDir = new TreeView();
            lstFileInfo = new ListBox();
            pnlCreate = new Panel();
            btnCreateAndOpen = new Button();
            txtNewFileName = new TextBox();
            lblNewFileName = new Label();
            txtCurrentDirectory = new TextBox();
            lblCurrentDirectory = new Label();
            tblSearchResults = new TableLayoutPanel();
            rtbFileText = new RichTextBox();
            pnlTop = new Panel();
            btnSearch = new Button();
            txtSearch = new TextBox();
            btnDirDown = new Button();
            btnDirUp = new Button();
            pnlBottom = new Panel();
            btnCreateFolder = new Button();
            btnDelete = new Button();
            btnCreateFile = new Button();
            btnSave = new Button();
            btnEdit = new Button();
            btnReadFile = new Button();
            pnlMaster.SuspendLayout();
            pnlMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlCreate.SuspendLayout();
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
            pnlMiddle.Controls.Add(splitContainer1);
            pnlMiddle.Dock = DockStyle.Fill;
            pnlMiddle.Location = new Point(0, 49);
            pnlMiddle.Name = "pnlMiddle";
            pnlMiddle.Padding = new Padding(10);
            pnlMiddle.Size = new Size(800, 350);
            pnlMiddle.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(10, 10);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.White;
            splitContainer1.Panel1.Controls.Add(tvDir);
            splitContainer1.Panel1.Margin = new Padding(3, 0, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.White;
            splitContainer1.Panel2.Controls.Add(lstFileInfo);
            splitContainer1.Panel2.Controls.Add(pnlCreate);
            splitContainer1.Panel2.Controls.Add(tblSearchResults);
            splitContainer1.Panel2.Controls.Add(rtbFileText);
            splitContainer1.Panel2.Margin = new Padding(0, 0, 3, 0);
            splitContainer1.Size = new Size(780, 330);
            splitContainer1.SplitterDistance = 331;
            splitContainer1.TabIndex = 0;
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
            lstFileInfo.IntegralHeight = false;
            lstFileInfo.ItemHeight = 15;
            lstFileInfo.Location = new Point(0, 0);
            lstFileInfo.Margin = new Padding(0);
            lstFileInfo.Name = "lstFileInfo";
            lstFileInfo.Size = new Size(445, 330);
            lstFileInfo.TabIndex = 0;
            // 
            // pnlCreate
            // 
            pnlCreate.BackColor = SystemColors.Control;
            pnlCreate.Controls.Add(btnCreateAndOpen);
            pnlCreate.Controls.Add(txtNewFileName);
            pnlCreate.Controls.Add(lblNewFileName);
            pnlCreate.Controls.Add(txtCurrentDirectory);
            pnlCreate.Controls.Add(lblCurrentDirectory);
            pnlCreate.Dock = DockStyle.Fill;
            pnlCreate.Location = new Point(0, 0);
            pnlCreate.Name = "pnlCreate";
            pnlCreate.Size = new Size(445, 330);
            pnlCreate.TabIndex = 7;
            // 
            // btnCreateAndOpen
            // 
            btnCreateAndOpen.Location = new Point(142, 142);
            btnCreateAndOpen.Name = "btnCreateAndOpen";
            btnCreateAndOpen.Size = new Size(161, 91);
            btnCreateAndOpen.TabIndex = 4;
            btnCreateAndOpen.Text = "Create and Open File";
            btnCreateAndOpen.UseVisualStyleBackColor = true;
            btnCreateAndOpen.Click += btnCreateAndOpen_Click;
            // 
            // txtNewFileName
            // 
            txtNewFileName.Location = new Point(106, 69);
            txtNewFileName.Name = "txtNewFileName";
            txtNewFileName.Size = new Size(334, 23);
            txtNewFileName.TabIndex = 3;
            // 
            // lblNewFileName
            // 
            lblNewFileName.AutoSize = true;
            lblNewFileName.Location = new Point(4, 72);
            lblNewFileName.Name = "lblNewFileName";
            lblNewFileName.Size = new Size(90, 15);
            lblNewFileName.TabIndex = 2;
            lblNewFileName.Text = "New File Name:";
            // 
            // txtCurrentDirectory
            // 
            txtCurrentDirectory.Location = new Point(106, 17);
            txtCurrentDirectory.Name = "txtCurrentDirectory";
            txtCurrentDirectory.Size = new Size(334, 23);
            txtCurrentDirectory.TabIndex = 1;
            // 
            // lblCurrentDirectory
            // 
            lblCurrentDirectory.AutoSize = true;
            lblCurrentDirectory.Location = new Point(4, 20);
            lblCurrentDirectory.Name = "lblCurrentDirectory";
            lblCurrentDirectory.Size = new Size(101, 15);
            lblCurrentDirectory.TabIndex = 0;
            lblCurrentDirectory.Text = "Current Directory:";
            // 
            // tblSearchResults
            // 
            tblSearchResults.AutoScroll = true;
            tblSearchResults.ColumnCount = 2;
            tblSearchResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblSearchResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblSearchResults.Dock = DockStyle.Fill;
            tblSearchResults.Location = new Point(0, 0);
            tblSearchResults.Name = "tblSearchResults";
            tblSearchResults.RowCount = 2;
            tblSearchResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblSearchResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblSearchResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblSearchResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblSearchResults.Size = new Size(445, 330);
            tblSearchResults.TabIndex = 1;
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
            pnlTop.Controls.Add(txtSearch);
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
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(361, 11);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(264, 25);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // btnDirDown
            // 
            btnDirDown.Location = new Point(153, 7);
            btnDirDown.Name = "btnDirDown";
            btnDirDown.Size = new Size(46, 33);
            btnDirDown.TabIndex = 5;
            btnDirDown.Text = "Down";
            btnDirDown.UseVisualStyleBackColor = true;
            btnDirDown.Click += btnDirDown_Click;
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
            pnlBottom.Controls.Add(btnCreateFolder);
            pnlBottom.Controls.Add(btnDelete);
            pnlBottom.Controls.Add(btnCreateFile);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Controls.Add(btnEdit);
            pnlBottom.Controls.Add(btnReadFile);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 399);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(800, 51);
            pnlBottom.TabIndex = 6;
            // 
            // btnCreateFolder
            // 
            btnCreateFolder.Location = new Point(288, 11);
            btnCreateFolder.Name = "btnCreateFolder";
            btnCreateFolder.Size = new Size(86, 29);
            btnCreateFolder.TabIndex = 8;
            btnCreateFolder.Text = "Create Folder";
            btnCreateFolder.UseVisualStyleBackColor = true;
            btnCreateFolder.Click += btnCreateFolder_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(104, 11);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(86, 29);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCreateFile
            // 
            btnCreateFile.Location = new Point(196, 11);
            btnCreateFile.Name = "btnCreateFile";
            btnCreateFile.Size = new Size(86, 29);
            btnCreateFile.TabIndex = 6;
            btnCreateFile.Text = "Create File";
            btnCreateFile.UseVisualStyleBackColor = true;
            btnCreateFile.Click += btnCreateFile_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(702, 11);
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
            btnEdit.Location = new Point(610, 11);
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
            btnReadFile.Location = new Point(12, 11);
            btnReadFile.Name = "btnReadFile";
            btnReadFile.Size = new Size(86, 29);
            btnReadFile.TabIndex = 3;
            btnReadFile.Text = "Read File";
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
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlCreate.ResumeLayout(false);
            pnlCreate.PerformLayout();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMaster;
        private SplitContainer splitContainer1;
        private Button btnReadFile;
        private Button btnSearch;
        private TextBox txtSearch;
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
        private TableLayoutPanel tblSearchResults;
        private Button btnCreateFile;
        private Panel pnlCreate;
        private Label lblNewFileName;
        private TextBox txtCurrentDirectory;
        private Label lblCurrentDirectory;
        private TextBox txtNewFileName;
        private Button btnCreateAndOpen;
        private Button btnDelete;
        private Button btnCreateFolder;
    }
}
