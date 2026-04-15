namespace Project_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FileOps.BuildFileTree(tvDir);
        }

        private void ShowControl(Control showControl)
        {

            rtbFileText.Clear();
            lstFileInfo.Items.Clear();

            rtbFileText.ReadOnly = true;

            lstFileInfo.Visible = false;
            rtbFileText.Visible = false;

            btnEdit.Visible = false;
            btnSave.Visible = false;

            if (showControl == rtbFileText)
            {
                btnEdit.Visible = true;
            }

            showControl.Visible = true;
        }

        private void tvDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = tvDir.SelectedNode.Tag as string;
            if (File.Exists(path))
            {
                ShowControl(lstFileInfo);
                FileOps.DisplayFileProperties(lstFileInfo, path);
            }

        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            string? file = tvDir.SelectedNode.Tag as string;

            if (string.IsNullOrEmpty(file))
            {
                MessageBox.Show("Error: Invalid file path.\nbtnReadFile_Click.");
            }
            if (!File.Exists(file))
            {
                MessageBox.Show("Error: File does not exist");
            }
            else if (!FileOps.IsFileTxt(file))
            {
                MessageBox.Show("Error: File is not text file.");
            }
            else
            {
                ShowControl(rtbFileText);
                rtbFileText.Text = FileOps.ReadFile(file);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            rtbFileText.ReadOnly = false;
            btnSave.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This save will overwite current file contents!",
                                                   "Do you want to continue?",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FileOps.WriteFile(rtbFileText.Text, tvDir.SelectedNode.Tag as string);
            }
        }

        private void btnDirUp_Click(object sender, EventArgs e)
        {
            ShowControl(lstFileInfo);
            
            string path = FileOps.GetParentDirectory(tvDir);

            tvDir.Nodes.Clear();

            FileOps.BuildFileTree(tvDir, path);
        }
    }
}
