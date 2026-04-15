using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public static void RecursiveAddDirectories(TreeNode node)
        {
            string path = node.Tag.ToString();

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(file));

                    fileNode.Tag = file;

                    node.Nodes.Add(fileNode);              
                }
            }

            foreach (string dir in Directory.GetDirectories(path))
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(dir));

                newNode.Tag = dir;

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

        /// <summary>
        /// Checks if path leads to a text file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        public static bool IsFileTxt(string path)
        {
            // Get file extension
            string ext = Path.GetExtension(path).ToLower();

            // Check if the file can be read as text (based on extension)
            if (ext == ".txt" || ext == ".cs" || ext == ".xml" || ext == ".csv" || ext == ".json")
            {
                return true;
            }
            return false; // If not text file 
        }

        /// <summary>
        /// Read contents of file at path and returns it as string
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string</returns>
        public static string ReadFile(string path)
        {
            return File.ReadAllText(path);
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



    }
}
