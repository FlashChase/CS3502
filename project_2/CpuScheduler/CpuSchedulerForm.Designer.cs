namespace CpuScheduler
{
    partial class CpuSchedulerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CpuSchedulerForm));
            panel1 = new System.Windows.Forms.Panel();
            sidePanel = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            btnWelcome = new System.Windows.Forms.Button();
            btnCpuScheduler = new System.Windows.Forms.Button();
            btnDashBoard = new System.Windows.Forms.Button();
            btnAbout = new System.Windows.Forms.Button();
            btnDarkModeToggle = new System.Windows.Forms.Button();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            restartApp = new System.Windows.Forms.Label();
            contentPanel = new System.Windows.Forms.Panel();
            schedulerPanel = new System.Windows.Forms.Panel();
            processDataGrid = new System.Windows.Forms.DataGridView();
            btnSetProcessCount = new System.Windows.Forms.Button();
            btnGenerateRandom = new System.Windows.Forms.Button();
            btnClearAll = new System.Windows.Forms.Button();
            btnSaveData = new System.Windows.Forms.Button();
            btnLoadData = new System.Windows.Forms.Button();
            algorithmButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            btnFCFS = new System.Windows.Forms.Button();
            btnSJF = new System.Windows.Forms.Button();
            btnPriority = new System.Windows.Forms.Button();
            btnRoundRobin = new System.Windows.Forms.Button();
            btnSJTRF = new System.Windows.Forms.Button();
            btnHRRN = new System.Windows.Forms.Button();
            cmbLoadExample = new System.Windows.Forms.ComboBox();
            txtProcess = new System.Windows.Forms.TextBox();
            labelProcess = new System.Windows.Forms.Label();
            welcomePanel = new System.Windows.Forms.Panel();
            comparisonPanel = new System.Windows.Forms.Panel();
            resultsPanel = new System.Windows.Forms.Panel();
            listView1 = new System.Windows.Forms.ListView();
            aboutPanel = new System.Windows.Forms.Panel();
            aboutTextBox = new System.Windows.Forms.RichTextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            btnLottery = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contentPanel.SuspendLayout();
            schedulerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)processDataGrid).BeginInit();
            algorithmButtonPanel.SuspendLayout();
            welcomePanel.SuspendLayout();
            resultsPanel.SuspendLayout();
            aboutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(sidePanel);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnWelcome);
            panel1.Controls.Add(btnCpuScheduler);
            panel1.Controls.Add(btnDashBoard);
            panel1.Controls.Add(btnAbout);
            panel1.Controls.Add(btnDarkModeToggle);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(restartApp);
            panel1.Location = new System.Drawing.Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(164, 600);
            panel1.TabIndex = 0;
            // 
            // sidePanel
            // 
            sidePanel.BackColor = System.Drawing.Color.SeaGreen;
            sidePanel.Location = new System.Drawing.Point(0, 161);
            sidePanel.Name = "sidePanel";
            sidePanel.Size = new System.Drawing.Size(10, 45);
            sidePanel.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label1.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(3, 506);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(158, 90);
            label1.TabIndex = 3;
            label1.Text = "© 2025.\r\nFork: Chris Regan\r\nKSU CS 3502 OS Class\r\n(OwlTech Training Tools)\r\n\r\nOriginal: Francis Nweke";
            // 
            // btnWelcome
            // 
            btnWelcome.BackColor = System.Drawing.SystemColors.InactiveBorder;
            btnWelcome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnWelcome.Cursor = System.Windows.Forms.Cursors.Hand;
            btnWelcome.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            btnWelcome.FlatAppearance.BorderSize = 0;
            btnWelcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnWelcome.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnWelcome.Location = new System.Drawing.Point(11, 135);
            btnWelcome.Name = "btnWelcome";
            btnWelcome.Size = new System.Drawing.Size(150, 40);
            btnWelcome.TabIndex = 0;
            btnWelcome.Text = "🏠   Welcome";
            btnWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnWelcome.UseVisualStyleBackColor = false;
            btnWelcome.Click += WelcomeButton_Click;
            // 
            // btnCpuScheduler
            // 
            btnCpuScheduler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            btnCpuScheduler.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCpuScheduler.FlatAppearance.BorderSize = 0;
            btnCpuScheduler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCpuScheduler.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnCpuScheduler.Image = (System.Drawing.Image)resources.GetObject("btnCpuScheduler.Image");
            btnCpuScheduler.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCpuScheduler.Location = new System.Drawing.Point(11, 180);
            btnCpuScheduler.Name = "btnCpuScheduler";
            btnCpuScheduler.Size = new System.Drawing.Size(150, 40);
            btnCpuScheduler.TabIndex = 2;
            btnCpuScheduler.Text = "    Scheduler";
            btnCpuScheduler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCpuScheduler.UseVisualStyleBackColor = true;
            btnCpuScheduler.Click += CpuSchedulerButton_Click;
            // 
            // btnDashBoard
            // 
            btnDashBoard.BackColor = System.Drawing.SystemColors.InactiveBorder;
            btnDashBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnDashBoard.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDashBoard.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            btnDashBoard.FlatAppearance.BorderSize = 0;
            btnDashBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDashBoard.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnDashBoard.Image = (System.Drawing.Image)resources.GetObject("btnDashBoard.Image");
            btnDashBoard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnDashBoard.Location = new System.Drawing.Point(11, 225);
            btnDashBoard.Name = "btnDashBoard";
            btnDashBoard.Size = new System.Drawing.Size(150, 45);
            btnDashBoard.TabIndex = 1;
            btnDashBoard.Text = "    Results";
            btnDashBoard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnDashBoard.UseVisualStyleBackColor = false;
            btnDashBoard.Click += DashBoardButton_Click;
            // 
            // btnAbout
            // 
            btnAbout.BackColor = System.Drawing.SystemColors.InactiveBorder;
            btnAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAbout.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            btnAbout.FlatAppearance.BorderSize = 0;
            btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAbout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnAbout.Location = new System.Drawing.Point(11, 270);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new System.Drawing.Size(150, 40);
            btnAbout.TabIndex = 3;
            btnAbout.Text = "📚   About";
            btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnAbout.UseVisualStyleBackColor = false;
            btnAbout.Click += AboutButton_Click;
            // 
            // btnDarkModeToggle
            // 
            btnDarkModeToggle.BackColor = System.Drawing.SystemColors.InactiveBorder;
            btnDarkModeToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnDarkModeToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDarkModeToggle.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            btnDarkModeToggle.FlatAppearance.BorderSize = 0;
            btnDarkModeToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDarkModeToggle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnDarkModeToggle.Location = new System.Drawing.Point(14, 406);
            btnDarkModeToggle.Name = "btnDarkModeToggle";
            btnDarkModeToggle.Size = new System.Drawing.Size(150, 30);
            btnDarkModeToggle.TabIndex = 4;
            btnDarkModeToggle.Text = "☀️ Light Mode";
            btnDarkModeToggle.UseVisualStyleBackColor = false;
            btnDarkModeToggle.Click += DarkModeToggle_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            pictureBox1.BackgroundImage = (System.Drawing.Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            pictureBox1.Cursor = System.Windows.Forms.Cursors.No;
            pictureBox1.Location = new System.Drawing.Point(0, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(164, 129);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // restartApp
            // 
            restartApp.BackColor = System.Drawing.SystemColors.InactiveBorder;
            restartApp.Cursor = System.Windows.Forms.Cursors.Hand;
            restartApp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            restartApp.ForeColor = System.Drawing.Color.DarkBlue;
            restartApp.Location = new System.Drawing.Point(3, 364);
            restartApp.Name = "restartApp";
            restartApp.Size = new System.Drawing.Size(150, 30);
            restartApp.TabIndex = 11;
            restartApp.Text = "🔄 Restart Application";
            restartApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            restartApp.Click += RestartApp_Click;
            // 
            // contentPanel
            // 
            contentPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            contentPanel.Controls.Add(schedulerPanel);
            contentPanel.Controls.Add(welcomePanel);
            contentPanel.Controls.Add(resultsPanel);
            contentPanel.Controls.Add(aboutPanel);
            contentPanel.Location = new System.Drawing.Point(171, 1);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new System.Drawing.Size(728, 600);
            contentPanel.TabIndex = 1;
            // 
            // schedulerPanel
            // 
            schedulerPanel.BackColor = System.Drawing.SystemColors.Control;
            schedulerPanel.Controls.Add(processDataGrid);
            schedulerPanel.Controls.Add(btnSetProcessCount);
            schedulerPanel.Controls.Add(btnGenerateRandom);
            schedulerPanel.Controls.Add(btnClearAll);
            schedulerPanel.Controls.Add(btnSaveData);
            schedulerPanel.Controls.Add(btnLoadData);
            schedulerPanel.Controls.Add(algorithmButtonPanel);
            schedulerPanel.Controls.Add(cmbLoadExample);
            schedulerPanel.Controls.Add(txtProcess);
            schedulerPanel.Controls.Add(labelProcess);
            schedulerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            schedulerPanel.Location = new System.Drawing.Point(0, 0);
            schedulerPanel.Name = "schedulerPanel";
            schedulerPanel.Padding = new System.Windows.Forms.Padding(10);
            schedulerPanel.Size = new System.Drawing.Size(728, 600);
            schedulerPanel.TabIndex = 1;
            schedulerPanel.Visible = false;
            // 
            // processDataGrid
            // 
            processDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            processDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            processDataGrid.Location = new System.Drawing.Point(20, 95);
            processDataGrid.Name = "processDataGrid";
            processDataGrid.RowHeadersWidth = 82;
            processDataGrid.Size = new System.Drawing.Size(450, 300);
            processDataGrid.TabIndex = 20;
            processDataGrid.CellContentClick += processDataGrid_CellContentClick;
            // 
            // btnSetProcessCount
            // 
            btnSetProcessCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSetProcessCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnSetProcessCount.Location = new System.Drawing.Point(330, 55);
            btnSetProcessCount.Name = "btnSetProcessCount";
            btnSetProcessCount.Size = new System.Drawing.Size(140, 30);
            btnSetProcessCount.TabIndex = 21;
            btnSetProcessCount.Text = "Set Process Count";
            btnSetProcessCount.UseVisualStyleBackColor = true;
            btnSetProcessCount.Click += SetProcessCount_Click;
            // 
            // btnGenerateRandom
            // 
            btnGenerateRandom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnGenerateRandom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnGenerateRandom.Location = new System.Drawing.Point(490, 95);
            btnGenerateRandom.Name = "btnGenerateRandom";
            btnGenerateRandom.Size = new System.Drawing.Size(120, 30);
            btnGenerateRandom.TabIndex = 22;
            btnGenerateRandom.Text = "Generate Random";
            btnGenerateRandom.UseVisualStyleBackColor = true;
            btnGenerateRandom.Click += GenerateRandom_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnClearAll.Location = new System.Drawing.Point(490, 135);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new System.Drawing.Size(120, 30);
            btnClearAll.TabIndex = 23;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += ClearAll_Click;
            // 
            // btnSaveData
            // 
            btnSaveData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnSaveData.Location = new System.Drawing.Point(490, 210);
            btnSaveData.Name = "btnSaveData";
            btnSaveData.Size = new System.Drawing.Size(120, 30);
            btnSaveData.TabIndex = 25;
            btnSaveData.Text = "💾 Save as CSV";
            btnSaveData.UseVisualStyleBackColor = true;
            btnSaveData.Click += SaveData_Click;
            // 
            // btnLoadData
            // 
            btnLoadData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnLoadData.Location = new System.Drawing.Point(490, 250);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new System.Drawing.Size(120, 30);
            btnLoadData.TabIndex = 26;
            btnLoadData.Text = "📂 Load CSV";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += LoadData_Click;
            // 
            // algorithmButtonPanel
            // 
            algorithmButtonPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            algorithmButtonPanel.Controls.Add(btnFCFS);
            algorithmButtonPanel.Controls.Add(btnSJF);
            algorithmButtonPanel.Controls.Add(btnPriority);
            algorithmButtonPanel.Controls.Add(btnRoundRobin);
            algorithmButtonPanel.Controls.Add(btnSJTRF);
            algorithmButtonPanel.Controls.Add(btnHRRN);
            algorithmButtonPanel.Controls.Add(btnLottery);
            algorithmButtonPanel.Location = new System.Drawing.Point(20, 407);
            algorithmButtonPanel.Name = "algorithmButtonPanel";
            algorithmButtonPanel.Size = new System.Drawing.Size(690, 120);
            algorithmButtonPanel.TabIndex = 25;
            // 
            // btnFCFS
            // 
            btnFCFS.BackColor = System.Drawing.Color.Beige;
            btnFCFS.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFCFS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnFCFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFCFS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnFCFS.Location = new System.Drawing.Point(10, 5);
            btnFCFS.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnFCFS.Name = "btnFCFS";
            btnFCFS.Size = new System.Drawing.Size(140, 50);
            btnFCFS.TabIndex = 8;
            btnFCFS.Text = "FCFS";
            btnFCFS.UseVisualStyleBackColor = false;
            btnFCFS.Click += FirstComeFirstServeButton_Click;
            // 
            // btnSJF
            // 
            btnSJF.BackColor = System.Drawing.Color.AntiqueWhite;
            btnSJF.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSJF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnSJF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSJF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnSJF.Location = new System.Drawing.Point(170, 5);
            btnSJF.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnSJF.Name = "btnSJF";
            btnSJF.Size = new System.Drawing.Size(140, 50);
            btnSJF.TabIndex = 9;
            btnSJF.Text = "SJF";
            btnSJF.UseVisualStyleBackColor = false;
            btnSJF.Click += ShortestJobFirstButton_Click;
            // 
            // btnPriority
            // 
            btnPriority.BackColor = System.Drawing.Color.Bisque;
            btnPriority.Cursor = System.Windows.Forms.Cursors.Hand;
            btnPriority.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnPriority.Location = new System.Drawing.Point(330, 5);
            btnPriority.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnPriority.Name = "btnPriority";
            btnPriority.Size = new System.Drawing.Size(140, 50);
            btnPriority.TabIndex = 10;
            btnPriority.Text = "PRIORITY";
            btnPriority.UseVisualStyleBackColor = false;
            btnPriority.Click += PriorityButton_Click;
            // 
            // btnRoundRobin
            // 
            btnRoundRobin.BackColor = System.Drawing.Color.PapayaWhip;
            btnRoundRobin.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRoundRobin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnRoundRobin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRoundRobin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnRoundRobin.Location = new System.Drawing.Point(490, 5);
            btnRoundRobin.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnRoundRobin.Name = "btnRoundRobin";
            btnRoundRobin.Size = new System.Drawing.Size(140, 50);
            btnRoundRobin.TabIndex = 12;
            btnRoundRobin.Text = "ROUND ROBIN";
            btnRoundRobin.UseVisualStyleBackColor = false;
            btnRoundRobin.Click += RoundRobinButton_Click;
            // 
            // btnSJTRF
            // 
            btnSJTRF.BackColor = System.Drawing.Color.PapayaWhip;
            btnSJTRF.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSJTRF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnSJTRF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSJTRF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnSJTRF.Location = new System.Drawing.Point(10, 65);
            btnSJTRF.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnSJTRF.Name = "btnSJTRF";
            btnSJTRF.Size = new System.Drawing.Size(140, 50);
            btnSJTRF.TabIndex = 13;
            btnSJTRF.Text = "SJTRF";
            btnSJTRF.UseVisualStyleBackColor = false;
            btnSJTRF.Click += btnSJTRF_Click;
            // 
            // btnHRRN
            // 
            btnHRRN.BackColor = System.Drawing.Color.PapayaWhip;
            btnHRRN.Cursor = System.Windows.Forms.Cursors.Hand;
            btnHRRN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnHRRN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHRRN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnHRRN.Location = new System.Drawing.Point(170, 65);
            btnHRRN.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnHRRN.Name = "btnHRRN";
            btnHRRN.Size = new System.Drawing.Size(140, 50);
            btnHRRN.TabIndex = 14;
            btnHRRN.Text = "HRRN";
            btnHRRN.UseVisualStyleBackColor = false;
            btnHRRN.Click += btnHRRN_Click;
            // 
            // cmbLoadExample
            // 
            cmbLoadExample.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cmbLoadExample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbLoadExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            cmbLoadExample.FormattingEnabled = true;
            cmbLoadExample.Items.AddRange(new object[] { "Load Example...", "Short Processes (1-5)", "Mixed Load (1-20)", "Heavy Load (10-30)", "Priority Demo" });
            cmbLoadExample.Location = new System.Drawing.Point(490, 175);
            cmbLoadExample.Name = "cmbLoadExample";
            cmbLoadExample.Size = new System.Drawing.Size(120, 23);
            cmbLoadExample.TabIndex = 24;
            cmbLoadExample.SelectedIndexChanged += LoadExample_SelectedIndexChanged;
            // 
            // txtProcess
            // 
            txtProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtProcess.Location = new System.Drawing.Point(200, 55);
            txtProcess.Name = "txtProcess";
            txtProcess.Size = new System.Drawing.Size(120, 26);
            txtProcess.TabIndex = 7;
            txtProcess.Text = "3";
            txtProcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txtProcess.TextChanged += ProcessTextBox_TextChanged;
            // 
            // labelProcess
            // 
            labelProcess.AutoSize = true;
            labelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelProcess.Location = new System.Drawing.Point(180, 30);
            labelProcess.Name = "labelProcess";
            labelProcess.Size = new System.Drawing.Size(168, 17);
            labelProcess.TabIndex = 6;
            labelProcess.Text = "Number of Processes:";
            // 
            // welcomePanel
            // 
            welcomePanel.BackColor = System.Drawing.SystemColors.Control;
            welcomePanel.Controls.Add(comparisonPanel);
            welcomePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            welcomePanel.Location = new System.Drawing.Point(0, 0);
            welcomePanel.Name = "welcomePanel";
            welcomePanel.Padding = new System.Windows.Forms.Padding(10);
            welcomePanel.Size = new System.Drawing.Size(728, 600);
            welcomePanel.TabIndex = 0;
            // 
            // comparisonPanel
            // 
            comparisonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            comparisonPanel.Location = new System.Drawing.Point(10, 10);
            comparisonPanel.Name = "comparisonPanel";
            comparisonPanel.Size = new System.Drawing.Size(708, 580);
            comparisonPanel.TabIndex = 0;
            comparisonPanel.Paint += comparisonPanel_Paint;
            // 
            // resultsPanel
            // 
            resultsPanel.BackColor = System.Drawing.SystemColors.Control;
            resultsPanel.Controls.Add(listView1);
            resultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            resultsPanel.Location = new System.Drawing.Point(0, 0);
            resultsPanel.Name = "resultsPanel";
            resultsPanel.Padding = new System.Windows.Forms.Padding(10);
            resultsPanel.Size = new System.Drawing.Size(728, 600);
            resultsPanel.TabIndex = 0;
            // 
            // listView1
            // 
            listView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listView1.BackColor = System.Drawing.SystemColors.Window;
            listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new System.Drawing.Point(10, 10);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(700, 482);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            // 
            // aboutPanel
            // 
            aboutPanel.BackColor = System.Drawing.SystemColors.Control;
            aboutPanel.Controls.Add(aboutTextBox);
            aboutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            aboutPanel.Location = new System.Drawing.Point(0, 0);
            aboutPanel.Name = "aboutPanel";
            aboutPanel.Padding = new System.Windows.Forms.Padding(10);
            aboutPanel.Size = new System.Drawing.Size(728, 600);
            aboutPanel.TabIndex = 2;
            aboutPanel.Visible = false;
            // 
            // aboutTextBox
            // 
            aboutTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            aboutTextBox.BackColor = System.Drawing.SystemColors.Window;
            aboutTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            aboutTextBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            aboutTextBox.Location = new System.Drawing.Point(15, 15);
            aboutTextBox.Name = "aboutTextBox";
            aboutTextBox.ReadOnly = true;
            aboutTextBox.Size = new System.Drawing.Size(698, 570);
            aboutTextBox.TabIndex = 0;
            aboutTextBox.Text = "";
            // 
            // btnLottery
            // 
            btnLottery.BackColor = System.Drawing.Color.PapayaWhip;
            btnLottery.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLottery.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            btnLottery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLottery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnLottery.Location = new System.Drawing.Point(330, 65);
            btnLottery.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            btnLottery.Name = "btnLottery";
            btnLottery.Size = new System.Drawing.Size(140, 50);
            btnLottery.TabIndex = 15;
            btnLottery.Text = "LOTTERY";
            btnLottery.UseVisualStyleBackColor = false;
            btnLottery.Click += btnLottery_Click;
            // 
            // CpuSchedulerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(900, 530);
            Controls.Add(contentPanel);
            Controls.Add(panel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(790, 543);
            Name = "CpuSchedulerForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Load += CpuSchedulerForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contentPanel.ResumeLayout(false);
            schedulerPanel.ResumeLayout(false);
            schedulerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)processDataGrid).EndInit();
            algorithmButtonPanel.ResumeLayout(false);
            welcomePanel.ResumeLayout(false);
            resultsPanel.ResumeLayout(false);
            aboutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnWelcome;
        private System.Windows.Forms.Button btnCpuScheduler;
        private System.Windows.Forms.Button btnDashBoard;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnDarkModeToggle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel welcomePanel;
        private System.Windows.Forms.Panel schedulerPanel;
        private System.Windows.Forms.Panel resultsPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label restartApp;
        private System.Windows.Forms.Button btnPriority;
        private System.Windows.Forms.Button btnSJF;
        private System.Windows.Forms.Button btnFCFS;
        public System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Button btnRoundRobin;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel aboutPanel;
        private System.Windows.Forms.RichTextBox aboutTextBox;
        private System.Windows.Forms.DataGridView processDataGrid;
        private System.Windows.Forms.Button btnSetProcessCount;
        private System.Windows.Forms.Button btnGenerateRandom;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.ComboBox cmbLoadExample;
        private System.Windows.Forms.FlowLayoutPanel algorithmButtonPanel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnComparison;
        private System.Windows.Forms.Panel comparisonPanel;
        private System.Windows.Forms.Button btnSJTRF;
        private System.Windows.Forms.Button btnHRRN;
        private System.Windows.Forms.Button btnLottery;
    }
}