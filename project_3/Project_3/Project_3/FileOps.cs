using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_3
{
    public class FileOps
    {
        /// <summary>
        /// Establishes the root node of the treeview and calls recursive function to add nodes
        /// </summary>
        /// <param name="tvDir"></param>
        public static void BuildTreeLevel(TreeView tvDir, string path = "")
        {

            tvDir.Nodes.Clear();

            if (path.Equals(""))
            {
                // Get path to current directory if not given
                path = Directory.GetCurrentDirectory();
            }

            // Create root node with filename display and full path as tag
            TreeNode rootNode = new TreeNode(Path.GetFileName(path));
            rootNode.Tag = path;
            rootNode.ImageKey = "folder";
            rootNode.SelectedImageKey = "folder";

            tvDir.Nodes.Add(rootNode);

            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            foreach (string directory in directories)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(directory));
                newNode.Tag = directory;
                newNode.ImageKey = "folder";
                newNode.SelectedImageKey = "folder";

                rootNode.Nodes.Add(newNode);

                try
                {
                    if (Directory.GetFiles(directory).Length < 1 && Directory.GetDirectories(directory).Length < 1)
                    {
                        continue;
                    }
                    else
                    {
                        newNode.Nodes.Add("DUMMY");
                    }
                }
                catch (UnauthorizedAccessException ua)
                {
                    continue;
                }
            }

            foreach (string file in files)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(file));
                newNode.Tag = file;
                newNode.ImageKey = "file";
                newNode.SelectedImageKey = "file";

                rootNode.Nodes.Add(newNode);
            }

            rootNode.Expand();
        }

        public static void BuildFullTree(TreeView tvDir, TreeNode node = null)
        {
            string rootPath;

            if (node == null)
            {
                rootPath = tvDir.Nodes[0].Tag.ToString();
            }
            else
            {
                rootPath = node.Tag.ToString();
            }

            // Create root node with filename display and full path as tag
            TreeNode rootNode = new TreeNode(Path.GetFileName(rootPath));
            rootNode.Tag = rootPath;
            rootNode.ImageKey = "folder";
            rootNode.SelectedImageKey = "folder";

            tvDir.Nodes.Clear();

            tvDir.Nodes.Add(rootNode);

            string[] files = Directory.GetFiles(rootPath);
            string[] directories = Directory.GetDirectories(rootPath);

            foreach (string directory in directories)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(directory));
                newNode.Tag = directory;
                newNode.ImageKey = "folder";
                newNode.SelectedImageKey = "folder";

                rootNode.Nodes.Add(newNode);

                try
                {
                    if (Directory.GetFiles(directory).Length < 1 && Directory.GetDirectories(directory).Length < 1)
                    {
                        continue;
                    }
                    else
                    {
                        RecursiveAddDirectories(newNode);
                    }
                }
                catch (UnauthorizedAccessException ua)
                {
                    continue;
                }
            }

            foreach (string file in files)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(file));
                newNode.Tag = file;
                newNode.ImageKey = "file";
                newNode.SelectedImageKey = "file";


                rootNode.Nodes.Add(newNode);
            }

            rootNode.Expand();
        }

        public static void AddChildren(TreeNode node)
        {

            if (node.Nodes[0].Text != "DUMMY") 
            {
                return; 
            }

            node.Nodes.Clear();

            string path = node.Tag.ToString();

            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (string directory in directories)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(directory));
                newNode.Tag = directory;

                node.Nodes.Add(newNode);

                try
                {
                    if (Directory.GetFiles(directory).Length < 1 && Directory.GetDirectories(directory).Length < 1)
                    {
                        continue;
                    }
                    else
                    {
                        newNode.Nodes.Add("DUMMY");
                    }
                }
                catch (UnauthorizedAccessException ua)
                {
                    continue;
                }                
            }

            foreach (string file in files)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(file));
                newNode.Tag = file;

                node.Nodes.Add(newNode);
            }
        }

        /// <summary>
        /// Recursively add files and directories to TreeView
        /// </summary>
        /// <param name="node"></param>
        public static void RecursiveAddDirectories(TreeNode node)
        {
            string path;

            // Store path
            if (node.Tag.ToString != null)
            {
                path = node.Tag.ToString();
            }
            else
            {
                return;
            }

            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (string directory in directories)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(directory));
                newNode.Tag = directory;

                node.Nodes.Add(newNode);

                try
                {
                    if (Directory.GetFiles(directory).Length < 1 && Directory.GetDirectories(directory).Length < 1)
                    {
                        continue;
                    }
                    else
                    {
                        RecursiveAddDirectories(newNode);
                    }
                }
                catch (UnauthorizedAccessException ua)
                {
                    continue;
                }
            }

            foreach (string file in files)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(file));
                newNode.Tag = file;

                node.Nodes.Add(newNode);
            }
            /*
            // Declare enumerable
            IEnumerable<string> files;

            try
            {
                files = Directory.EnumerateFiles(path);
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(file));

                    fileNode.Tag = file;

                    node.Nodes.Add(fileNode);
                }
            }

            IEnumerable<string> folders;
            try
            {
                folders = Directory.EnumerateDirectories(path);
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }

            foreach (string folder in folders)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(folder));

                newNode.Tag = folder;

                node.Nodes.Add(newNode);

                RecursiveAddDirectories(newNode);
            }
            */
        }

        /// <summary>
        /// Returns the path to the parent directory of the root node
        /// </summary>
        /// <param name="tvDir"></param>
        /// <returns>string</returns>
        public static string GetParentDirectory(TreeView tvDir)
        {
            string currentDir = tvDir.Nodes[0].Tag.ToString();

            try
            {
                string parentDir = Directory.GetParent(currentDir).FullName;
                return parentDir;
            }
            catch (NullReferenceException nu)
            {
                return null;
            }

            return null;
        }

        public static string GetFirstSubDirectory(TreeView tvDir, int index)
        {
            TreeNode node;
            try
            {
                node = tvDir.Nodes[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Directory contains no subdirectories");
                return null;
            }


            foreach (TreeNode child in node.Nodes)
            {
                if (Directory.Exists(child.Tag.ToString()))
                {
                    return child.Tag.ToString();
                }
            }

            GetFirstSubDirectory(tvDir, ++index);


            return null;
        }

        public static string GetCurrentDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return path;
                }
                else if (File.Exists(path))
                {
                    return Path.GetDirectoryName(path);
                }
                else
                {
                    return null;
                }
            }
            catch (PathTooLongException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Checks if path leads to a text file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        public static bool IsFileTxt(string path)
        {
            // Get file extension
            string ext = Path.GetExtension(path).ToLower();

            // Exclude common non-text files
            if (ext == ".exe" || ext == ".png" || ext == ".jpg" || ext == ".dll" || ext == ".zip" ||
                ext == ".pdf" || ext == ".mp4" || ext == ".pdb" || ext == ".docx" || ext == "xlsx")
            {
                return false;
            }
            return true; // If not common non-text file type
        }

        /// <summary>
        /// Read contents of file at path and returns it as string
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string</returns>
        public static string ReadFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        return File.ReadAllText(path);
                    }
                    catch (IOException io)
                    {
                        MessageBox.Show(io.Message);
                        return null;
                    }
                }
                else if (Directory.Exists(path))
                {
                    return null;
                }
                else
                {
                    MessageBox.Show("Error: Could not find file on path");
                    return null;
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("File Access Denied");
                return null;
            }
        }

        /// <summary>
        /// Adds the filename, filesize, and creation date fo the file at path to a list box
        /// </summary>
        /// <param name="lb"></param>
        /// <param name="path"></param>
        public static void DisplayFileProperties(ListBox lb, string path)
        {
            var fileInfo = new FileInfo(path);
            float fileSize = fileInfo.Length;
            string unitType = "bytes";

            if (fileSize >= 1_000_000)
            {
                unitType = "MB";
                fileSize /= 1_000_000;
            }
            else if (fileSize >= 1000)
            {
                unitType = "Kilobytes";
                fileSize /= 1000;
            }

            lb.Items.Clear();
            lb.Items.Add("Filename: " + fileInfo.Name);
            lb.Items.Add($"Size: {fileSize:F2} {unitType}");
            lb.Items.Add("Created:  " + fileInfo.CreationTime.ToString());

        }

        /// <summary>
        /// Write given text to file at path
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void WriteFile(string text, TreeNode node)
        {
            string path = node.Tag.ToString();
            try
            {
                File.WriteAllText(path, text);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Error: Cannot access " + path);
            }

        }

        /// <summary>
        ///  Search nodes for file names containing user search criteria.
        ///  Matches are entered into list of TreeNodes and returned.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="root"></param>
        /// <returns>List<TreeNode></returns>
        public static void SearchForFile(string str, TreeNode root, List<TreeNode> matchList)
        {
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Text.ToLower().Contains(str.ToLower()))
                {
                    matchList.Add(node);
                }
                SearchForFile(str, node, matchList);
            }
        }

        public static TreeNode CreateFile(string fileName, string directoryPath, TreeNodeCollection nodes)
        {
            // Give file .txt extension if it doesn't have a . ending
            if (!Path.HasExtension(fileName))
            {
                fileName += ".txt";
            }

            string filePath;

            filePath = Path.Combine(directoryPath, fileName);
  
            try
            {
                if (File.Exists(filePath))
                {
                    var choice = MessageBox.Show("File exists on the current path! Do you want to overwrite it?", "Confirm",
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (choice == DialogResult.Yes)
                    {
                        filePath = Path.Combine(directoryPath, fileName);

                        using (File.Create(filePath)) ;

                        return FindNode(nodes, filePath);                      
                    }
                }

                // Check if current path is a valid directory
                else if (Directory.Exists(directoryPath) || Directory.Exists(directoryPath + "'\'"))
                {
                    filePath = Path.Combine(directoryPath, fileName);

                    // using opens and closes a file stream
                    using (File.Create(filePath));

                    TreeNode? dirNode = FindNode(nodes, directoryPath);

                    if (dirNode != null)
                    {
                        TreeNode newNode = dirNode.Nodes.Add(fileName);
                        newNode.Tag = filePath;
                        return newNode;
                    }                    
                }
                else
                {
                    MessageBox.Show($"No valid directory in\n{directoryPath}\nto create file");
                }
            }
            catch (PathTooLongException tl)
            {
                MessageBox.Show("File not created. Path exceeded maximum length");
                MessageBox.Show(tl.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public static TreeNode CreateDirectory(string directoryName, string directoryPath, TreeNodeCollection nodes)
        {
            string newdirectoryPath;

            newdirectoryPath = Path.Combine(directoryPath, directoryName);

            try
            {            
                // Check if current path is a valid directory
                if (Directory.Exists(directoryPath) || Directory.Exists(directoryPath + "'\'"))
                {
                    newdirectoryPath = Path.Combine(directoryPath, directoryName);

                    Directory.CreateDirectory(newdirectoryPath);

                    TreeNode? dirNode = FindNode(nodes, directoryPath);

                    if (dirNode != null)
                    {
                        TreeNode newNode = dirNode.Nodes.Add(directoryName);
                        newNode.Tag = newdirectoryPath;
                        return newNode;
                    }
                }
                else
                {
                    MessageBox.Show($"No valid directory in\n{directoryPath}\nto create file");
                }
            }
            catch (PathTooLongException tl)
            {
                MessageBox.Show("File not created. Path exceeded maximum length");
                MessageBox.Show(tl.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public static void DeleteFile(TreeNode node)
        {
            string path = node.Tag.ToString();
            int fails = 0;

            if (File.Exists(path))
            {
                string fileName = Path.GetFileName(path);
                DialogResult result = MessageBox.Show("Are you sure you want to delete " + fileName + "?", "Delete File",
                                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(path);
                        node.Remove();
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        fails++;
                        MessageBox.Show(e.Message);
                    }
                }
                else { return; }
            }
            else if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                string[] subDir = Directory.GetDirectories(path);

                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        node.Remove();
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        fails++;
                        MessageBox.Show(e.Message);
                    }
                }
                foreach (string dir in subDir)
                {
                    fails += RecursiveDelete(dir);
                }

                if (fails == 0)
                {
                    try
                    {
                        Directory.Delete(path);
                    }
                    catch (IOException io)
                    {
                        MessageBox.Show(io.Message);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Directory contains multiple files that cannot be deleted");
                }
            }
        }

        private static int RecursiveDelete(string path)
        {

            try
            {
                Directory.Delete(path, true);
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 1;
            }
            /*
            int fails = 0;
            string[] files = Directory.GetFiles(path);
            string[] subDir = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (UnauthorizedAccessException e)
                {
                    fails++;
                    MessageBox.Show(e.Message);
                }
            }

            foreach (string dir in subDir)
            {
                fails += RecursiveDelete(dir);
                if (fails == 0)
                {
                    Directory.Delete(dir);
                }
            }

            return fails;
            */
        }

        public static TreeNode UpdateSelectedNode(TreeNodeCollection nodes, string filePath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString().Equals(filePath))
                {
                    return node;
                }
            }
            return null;
        }

        public static TreeNode FindNode(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag == null) {
                    continue;
                }
                else if(node.Tag.ToString().Equals(path))
                {
                    return node;
                }
                else if (Directory.Exists(node.Tag.ToString()))
                {
                    TreeNode? match = null;
                    match = FindNode(node.Nodes, path);
                    if (match != null)
                    {
                        return match;
                    }
                }
            }
            return null;
        }
    }
}
