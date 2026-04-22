using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_3
{
    public class FileOps
    {
        /// <summary>
        /// Establishes the root node of the treeview and calls recursive function to add nodes
        /// </summary>
        /// <param name="tvDir"></param>
        public static void BuildFileTree(TreeView tvDir, string path = "")
        {
            if (path.Equals(""))
            {
                // Get path to current directory if not given
                path = Directory.GetCurrentDirectory();
            }
           
            // Create root node with filename display and full path as tag
            TreeNode rootNode = new TreeNode(Path.GetFileName(path));
            rootNode.Tag = path;

            // Add the root node to the TreeView
            tvDir.Nodes.Add(rootNode);
                      
            // Pass the root node to function to add sister & child nodes
            RecursiveAddDirectories(rootNode);

            // Start TreeView with the root folder expanded
            rootNode.Expand();
        }

        /// <summary>
        /// Recursively add files and directories to TreeView
        /// </summary>
        /// <param name="node"></param>
        public static void RecursiveAddDirectories(TreeNode node)
        {
            // Store path
            string path = node.Tag.ToString();

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
        }

        /// <summary>
        /// Returns the path to the parent directory of the root node
        /// </summary>
        /// <param name="tvDir"></param>
        /// <returns>string</returns>
        public static string GetParentDirectory(TreeView tvDir)
        {
            string currentDir = tvDir.Nodes[0].Tag.ToString();

            string parentDir = Directory.GetParent(currentDir).FullName;

            return parentDir;
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
                ext == ".pdf" || ext == ".mp4" || ext == ".pdb" || ext == ".docx" || ext == "xlsx" ||
                )
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
                    return File.ReadAllText(path);
                }
                else if (Directory.Exists(path))
                {
                    return "90210Error&";
                }
                else
                {
                    MessageBox.Show("Error: Could not find file on path");
                    return "90210Error&";
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("File Access Denied");
                return "90210Error&";
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
        public static void WriteFile(string text, string path)
        {
            File.WriteAllText(path, text);
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
            foreach (TreeNode node in root.Nodes) {
                if (node.Text.ToLower().Contains(str.ToLower())) {
                    matchList.Add(node);                  
                }
                SearchForFile(str, node, matchList);
            }
        }


    }
}
