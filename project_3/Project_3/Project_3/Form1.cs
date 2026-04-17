using System.Windows.Forms;

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
            tblSearchResults.Controls.Clear();

            rtbFileText.ReadOnly = true;

            lstFileInfo.Visible = false;
            rtbFileText.Visible = false;
            tblSearchResults.Visible = false;

            btnEdit.Visible = false;
            btnSave.Visible = false;

            if (showControl == rtbFileText)
            {
                btnEdit.Visible = true;
            }

            showControl.BringToFront();
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
            btnReadFile.Visible = true;

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

        private void btnDirDown_Click(object sender, EventArgs e)
        {
            ShowControl(lstFileInfo);

            string path = null;

            if (tvDir.SelectedNode != null && tvDir.SelectedNode.Tag != null && Directory.Exists(tvDir.SelectedNode.Tag.ToString()))
            {
                path = tvDir.SelectedNode.Tag.ToString();
                tvDir.Nodes.Clear();
                FileOps.BuildFileTree(tvDir, path);

            }
            else
            {
                path = FileOps.GetFirstSubDirectory(tvDir, 0);
                if (path != null)
                {
                    tvDir.Nodes.Clear();
                    FileOps.BuildFileTree(tvDir, path);
                }
            }
            
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowControl(tblSearchResults);

            List<TreeNode> searchResults = new List<TreeNode>();
            FileOps.SearchForFile(txtSearch.Text, tvDir.Nodes[0], searchResults);

            if (searchResults.Count() == 0)
            {
                MessageBox.Show("Search returned no results");
            }
            else
            {
                DisplaySearchResults(searchResults);
            }
        }

        /// <summary>
        /// Display the results matching user search
        /// </summary>
        /// <param name="searchResults"></param>
        private void DisplaySearchResults(List<TreeNode> searchResults)
        {
            Label label = new Label();
            label.Text = "Name";
            tblSearchResults.Controls.Add(label, 0, 0);

            Label label2 = new Label();
            label2.Text = "Folder";
            tblSearchResults.Controls.Add(label2, 1, 0);

            for (int row = 1; row <= searchResults.Count; row++)
            {
                Label nameLabel = new Label();
                nameLabel.DoubleClick += lblSearchResults_DoubleClick;
                nameLabel.Text = Path.GetFileName(searchResults[row - 1].Tag.ToString());
                nameLabel.Tag = searchResults[row - 1].Tag.ToString();
                tblSearchResults.Controls.Add(nameLabel, 0, row);

                for (int col = 1; col < 2; col++)
                {
                    Label typeLabel = new Label();
                    typeLabel.DoubleClick += lblSearchResults_DoubleClick;
                    typeLabel.Text = Path.GetFileNameWithoutExtension(searchResults[row - 1].Parent.Tag.ToString());
                    tblSearchResults.Controls.Add(typeLabel, col, row);
                }
            }
        }

        private void lblSearchResults_DoubleClick(object sender, EventArgs e)
        {
            ShowControl(rtbFileText);

            Label label = (Label)sender;

            rtbFileText.Text = FileOps.ReadFile(label.Tag.ToString());

            btnReadFile.Visible = false;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
                
    }
}
