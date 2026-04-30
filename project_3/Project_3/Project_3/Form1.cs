using System.IO;
using System.Windows.Forms;

namespace Project_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Shown += Form1_Shown;

            tvDir.BeforeExpand += tvDir_BeforeExpand;
            tvDir.NodeMouseDoubleClick += tvDir_NodeMouseDoubleClick;
            tvDir.AfterSelect += tvDir_AfterSelect;
        }

        TreeNode? currentDirectoryNode;

        private void Form1_Shown(object Sender, EventArgs e)
        {
            try
            {
                ImageList imageList = new ImageList();
                imageList.Images.Add("folder", Image.FromFile("FolderIcon.png"));
                imageList.Images.Add("file", Image.FromFile("FileIcon.png"));
                tvDir.ImageList = imageList;

                btnDirUp.Image = Image.FromFile("ArrowUp.png");
                btnDirDown.Image = Image.FromFile("ArrowDown.png");

                FileOps.BuildTreeLevel(tvDir);
                currentDirectoryNode = tvDir.Nodes[0];
                tvDir.SelectedNode = tvDir.Nodes[0];

                lstFileInfo.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "BuildTreeLevel Error");
            }
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
            pnlCreate.Visible = false;

            btnEdit.Visible = false;
            btnSave.Visible = false;

            if (showControl == rtbFileText)
            {
                btnEdit.Visible = true;
                this.AcceptButton = btnEdit;
            }

            showControl.BringToFront();
            showControl.Visible = true;
        }

        private void tvDir_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            tvDir.SelectedNode = e.Node;

            FileOps.AddChildren(e.Node);

            tvDir.Refresh();
        }

        private void tvDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tblSearchResults.Visible)
            {
                return;
            }
            string path = tvDir.SelectedNode.Tag as string;
            string name = tvDir.SelectedNode.Text;
            lblNode.Text = name;
            if (File.Exists(path))
            {
                currentDirectoryNode = tvDir.SelectedNode.Parent;
                ShowControl(lstFileInfo);
                FileOps.DisplayFileProperties(lstFileInfo, path);
                btnReadFile.Visible = true;
            }
            else if (Directory.Exists(path))
            {
                btnReadFile.Visible = false;
                currentDirectoryNode = tvDir.SelectedNode;
                ShowControl(lstFileInfo);
                
            }
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            ReadFile(tvDir.SelectedNode);
        }

        private void ReadFile(TreeNode node)
        {
            string? file = node.Tag as string;

            if (string.IsNullOrEmpty(file))
            {
                MessageBox.Show("Error: Invalid file path.\nbtnReadFile_Click.");
                return;
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
                string? fileText = FileOps.ReadFile(file);
                if (fileText != null)
                {
                    ShowControl(rtbFileText);
                    rtbFileText.Text = fileText;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            rtbFileText.ReadOnly = false;
            btnSave.Visible = true;
            this.AcceptButton = btnSave;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This save will overwite current file contents!",
                                                   "Do you want to continue?",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                TreeNode? node = tvDir.SelectedNode;
                if (node != null)
                {
                    FileOps.WriteFile(rtbFileText.Text, node);
                }
                else
                {
                    node = tvDir.Nodes[0];
                    FileOps.WriteFile(rtbFileText.Text, node);
                }
                tvDir.Refresh();
            }
        }

        private void btnDirUp_Click(object sender, EventArgs e)
        {
            ShowControl(lstFileInfo);

            string? path = FileOps.GetParentDirectory(tvDir);

            if (path != null)
            {

                tvDir.Nodes.Clear();

                FileOps.BuildTreeLevel(tvDir, path);
                tvDir.SelectedNode = tvDir.Nodes[0];
            }
            else
            {
                MessageBox.Show("Already at the root of the file structure");
            }
        }

        private void btnDirDown_Click(object sender, EventArgs e)
        {
            ShowControl(lstFileInfo);

            string path;

            if (tvDir.SelectedNode == null)
            {
                path = FileOps.GetFirstSubDirectory(tvDir, 0);
                if (path != null)
                {
                    tvDir.Nodes.Clear();
                    FileOps.BuildTreeLevel(tvDir, path);
                    tvDir.SelectedNode = tvDir.Nodes[0];
                }
                return;
            }

            path = tvDir.SelectedNode.Tag.ToString();

            if (tvDir.SelectedNode == tvDir.Nodes[0])
            {
                path = FileOps.GetFirstSubDirectory(tvDir, 0);
                if (path != null)
                {
                    FileOps.BuildTreeLevel(tvDir, path);
                    tvDir.SelectedNode = tvDir.Nodes[0];
                }
            }
            else if (File.Exists(path))
            {
                MessageBox.Show("Selected file is not a directory");
            }
            else if (Directory.Exists(path))
            {
                FileOps.BuildTreeLevel(tvDir, path);
                tvDir.SelectedNode = tvDir.Nodes[0];
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("No search criteria entered");
                return;
            }
            ShowControl(tblSearchResults);

            FileOps.BuildFullTree(tvDir, currentDirectoryNode);

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
            // Create header labels
            Label label = new Label();
            label.Text = "Name";
            tblSearchResults.Controls.Add(label, 0, 0);

            Label label2 = new Label();
            label2.Text = "Folder";
            tblSearchResults.Controls.Add(label2, 1, 0);

            // Iterate of each cell in the table and add info from search results
            for (int row = 1; row <= searchResults.Count; row++)
            {
                tblSearchResults.RowCount++;
                tblSearchResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

                Label nameLabel = new Label();
                nameLabel.DoubleClick += lblSearchResults_DoubleClick;
                nameLabel.Text = Path.GetFileName(searchResults[row - 1].Tag.ToString());
                nameLabel.Tag = searchResults[row - 1].Tag.ToString();
                nameLabel.AutoSize = false;
                nameLabel.Dock = DockStyle.Fill;
                nameLabel.TextAlign = ContentAlignment.MiddleLeft;
                tblSearchResults.Controls.Add(nameLabel, 0, row);

                for (int col = 1; col < 2; col++)
                {
                    Label typeLabel = new Label();
                    typeLabel.DoubleClick += lblSearchResults_DoubleClick;
                    typeLabel.Text = Path.GetFileNameWithoutExtension(searchResults[row - 1].Parent.Tag.ToString());
                    typeLabel.Tag = searchResults[row - 1].Tag.ToString();
                    typeLabel.AutoSize = false;
                    typeLabel.Dock = DockStyle.Fill;
                    typeLabel.TextAlign = ContentAlignment.MiddleLeft;
                    tblSearchResults.Controls.Add(typeLabel, col, row);
                }
            }

        }

        private void lblSearchResults_DoubleClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;           

            this.AcceptButton = btnEdit;

            TreeNode node = FileOps.FindNode(tvDir.Nodes, label.Tag.ToString());

            tvDir.SelectedNode = node;
            node.EnsureVisible();
            tvDir.Focus();

            if (File.Exists(node.Tag.ToString()))
            {
                ReadFile(node);
                btnReadFile.Visible = false;
            }
            else if (Directory.Exists(node.Tag.ToString()))
            {
                ShowControl(lstFileInfo);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (tvDir.SelectedNode == null)
            {
                txtCurrentDirectory.Text = FileOps.GetCurrentDirectory(tvDir.Nodes[0].Tag.ToString());
            }
            else
            {
                txtCurrentDirectory.Text = FileOps.GetCurrentDirectory(tvDir.SelectedNode.Tag.ToString());
            }

            lblNewFileName.Text = "New Filename:";
            btnCreateAndOpen.Text = "Create and open file";
            ShowControl(pnlCreate);

            txtNewFileName.Focus();
            this.AcceptButton = btnCreateAndOpen;
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            if (tvDir.SelectedNode == null)
            {
                txtCurrentDirectory.Text = FileOps.GetCurrentDirectory(tvDir.Nodes[0].Tag.ToString());
            }
            else
            {
                txtCurrentDirectory.Text = FileOps.GetCurrentDirectory(tvDir.SelectedNode.Tag.ToString());
            }

            lblNewFileName.Text = "New Folder Name:";
            btnCreateAndOpen.Text = "Create and open folder";
            ShowControl(pnlCreate);

            txtNewFileName.Focus();
            this.AcceptButton = btnCreateAndOpen;
        }



        private void btnCreateAndOpen_Click(object sender, EventArgs e)
        {
            if (btnCreateAndOpen.Text.Contains("file"))
            {
                // Store contents of filename textbox
                string fileName = txtNewFileName.Text;

                // Ensure file name was entered
                if (fileName == "")
                {
                    MessageBox.Show("File must have a name");
                    return;
                }

                TreeNode? newNode = null;
                newNode = FileOps.CreateFile(txtNewFileName.Text, txtCurrentDirectory.Text, tvDir.Nodes);
                tvDir.SelectedNode = newNode;
                tvDir.Refresh();
                txtNewFileName.Clear();

                if (tvDir.SelectedNode != null)
                {
                    ShowControl(rtbFileText);
                    rtbFileText.ReadOnly = false;
                    rtbFileText.Focus();
                    btnSave.Visible = true;
                    this.AcceptButton = btnSave;
                }
            }
            else
            {
                // Store contents of filename textbox
                string directoryName = txtNewFileName.Text;

                // Ensure file name was entered
                if (directoryName == "")
                {
                    MessageBox.Show("Directory must have a name");
                    return;
                }

                TreeNode? newNode = null;
                newNode = FileOps.CreateDirectory(txtNewFileName.Text, txtCurrentDirectory.Text, tvDir.Nodes);
                tvDir.SelectedNode = newNode;
                tvDir.Refresh();
                txtNewFileName.Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = tvDir.SelectedNode;
            if (node != null)
            {
                FileOps.DeleteFile(tvDir.SelectedNode);
                tvDir.Refresh();
            }
            else
            {
                MessageBox.Show("No file or folder selected");
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = btnSearch;
        }

        private void tvDir_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            string path = node.Tag.ToString();

            if (Directory.Exists(path))
            {
                FileOps.BuildTreeLevel(tvDir, path);
            }
            else if (File.Exists(path))
            {
                ReadFile(node);
            }
        }

    }
}
