using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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

            string[] files;
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

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

        /// <summary>
        /// Builds the full file tree in the TreeView from argument note or TreeView root
        /// </summary>
        /// <param name="tvDir"></param>
        /// <param name="node"></param>
        public static void BuildFullTree(TreeView tvDir, TreeNode node = null)
        {
            string rootPath;

            if (node == null)
            {
                if (tvDir.Nodes.Count == 0 || tvDir.Nodes[0].Tag == null) { return; }
                rootPath = tvDir.Nodes[0].Tag.ToString();
            }
            else
            {
                if (node.Tag == null) { return; }
                rootPath = node.Tag.ToString();
            }

            // Create root node with filename display and full path as tag
            TreeNode rootNode = new TreeNode(Path.GetFileName(rootPath));
            rootNode.Tag = rootPath;
            rootNode.ImageKey = "folder";
            rootNode.SelectedImageKey = "folder";

            tvDir.Nodes.Clear();

            tvDir.Nodes.Add(rootNode);

            string[] files;
            string[] directories;
            try
            {   
                files = Directory.GetFiles(rootPath);
                directories = Directory.GetDirectories(rootPath);
            }          
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

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

        /// <summary>
        /// Adds one level of children to the given node.
        /// </summary>
        /// <param name="node"></param>
        public static void AddChildren(TreeNode node)
        {
            if (node.Nodes.Count == 0) { return; }

            if (node.Nodes[0].Text != "DUMMY") { return; }

            if (node.Tag == null) { return; }

            node.Nodes.Clear();


            string path = node.Tag.ToString();

            string[] directories;
            string[] files;

            try
            {
                directories = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            foreach (string directory in directories)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(directory));
                newNode.Tag = directory; 
                newNode.ImageKey = "folder";
                newNode.SelectedImageKey = "folder";

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
                newNode.ImageKey = "file";
                newNode.SelectedImageKey = "file";

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

            // Validate and store path
            if (node.Tag == null) { return; }

            else {  path = node.Tag.ToString(); }

            string[] directories;
            string[] files;

            try
            {
                directories = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            foreach (string directory in directories)
            {
                TreeNode newNode = new TreeNode(Path.GetFileName(directory));
                newNode.Tag = directory;
                newNode.ImageKey = "folder";
                newNode.SelectedImageKey = "folder";

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
                newNode.ImageKey = "file";
                newNode.SelectedImageKey = "file";

                node.Nodes.Add(newNode);
            }
        }

        /// <summary>
        /// Gets the parent directory of the TreeView root node
        /// </summary>
        /// <param name="tvDir"></param>
        /// <returns>Returns the path to the parent directory of the root node</returns>
        public static string GetParentDirectory(TreeView tvDir)
        {
            if (tvDir.Nodes.Count == 0 || tvDir.Nodes[0].Tag == null) { return null; }

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

        /// <summary>
        /// Searches children of node and returns first sub directory found
        /// </summary>
        /// <param name="tvDir"></param>
        /// <param name="index"></param>
        /// <returns>Path of first sub directory found</returns>
        public static string GetFirstSubDirectory(TreeView tvDir, int index)
        {
            if (index >= tvDir.Nodes.Count) { return null; }

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
                if (child.Tag != null && Directory.Exists(child.Tag.ToString()))
                {
                    return child.Tag.ToString();
                }
            }

            return GetFirstSubDirectory(tvDir, ++index);
        }

        /// <summary>
        /// Gets the full path of the directory containing the given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Fullpath of the directory the given path is located</returns>
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
                ext == ".pdf" || ext == ".mp4" || ext == ".pdb" || ext == ".docx" || ext == ".xlsx")
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
            if (!File.Exists(path)) { return; }

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
            if (node.Tag == null) return;

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

        /// <summary>
        /// Creates file with and TreeNode using give path
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="directoryPath"></param>
        /// <param name="nodes"></param>
        /// <returns>TreeNode containing the name and fullpath of the created file</returns>
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

                        TreeNode newNode = FindNode(nodes, filePath);
                        newNode.ImageKey = "file";
                        newNode.SelectedImageKey = "file";
                        return FindNode(nodes, filePath);                      
                    }
                }

                // Check if current path is a valid directory
                else if (Directory.Exists(directoryPath))
                {
                    filePath = Path.Combine(directoryPath, fileName);

                    // using opens and closes a file stream
                    using (File.Create(filePath));

                    TreeNode? dirNode = FindNode(nodes, directoryPath);

                    if (dirNode != null)
                    {
                        TreeNode newNode = dirNode.Nodes.Add(fileName);
                        newNode.Tag = filePath;
                        newNode.ImageKey = "file";
                        newNode.SelectedImageKey = "file";
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

        /// <summary>
        /// Creates a directory and TreeNode using the given path
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="directoryPath"></param>
        /// <param name="nodes"></param>
        /// <returns>TreeNode containing the directory name and fullpath of the created directory</returns>
        public static TreeNode CreateDirectory(string directoryName, string directoryPath, TreeNodeCollection nodes)
        {
            string newdirectoryPath;

            newdirectoryPath = Path.Combine(directoryPath, directoryName);

            try
            {            
                // Check if current path is a valid directory
                if (Directory.Exists(directoryPath))
                {
                    newdirectoryPath = Path.Combine(directoryPath, directoryName);

                    Directory.CreateDirectory(newdirectoryPath);

                    TreeNode? dirNode = FindNode(nodes, directoryPath);

                    if (dirNode != null)
                    {
                        TreeNode newNode = dirNode.Nodes.Add(directoryName);
                        newNode.Tag = newdirectoryPath;
                        newNode.ImageKey = "folder";
                        newNode.SelectedImageKey = "folder";
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

        /// <summary>
        /// Deletes the file or directory at the full path given the the TreeNode Tag
        /// If path leads to a directory, attempt to recursively delete all contained files and directories
        /// </summary>
        /// <param name="node"></param>
        public static void DeleteFile(TreeNode node)
        {
            if (node.Tag == null) return;
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
                        return;
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
                else { return; }
            }
            else if (Directory.Exists(path))
            {
                fails = RecursiveDelete(node);                
            }

            if (fails > 0)
            {
                MessageBox.Show($"Could not delete {node.Text}, {fails} files/directories inside could not be deleted");
            }
        }

        /// <summary>
        /// Recursively delete all files and directories at the full path of the given node, then delete the node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Number of files and directories which were unable to be deleted</returns>
        private static int RecursiveDelete(TreeNode node)
        {
            int fails = 0;

            if (node.Nodes.Count == 0)
            {
                if (node.Tag != null && File.Exists(node.Tag.ToString()))
                {
                    try
                    {
                        File.Delete(node.Tag.ToString());
                        node.Remove();
                        return fails;
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show(e.Message);
                        return ++fails;
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show(e.Message);
                        return ++fails;
                    }
                }
                else
                {
                    if (node.Tag != null && Directory.Exists(node.Tag.ToString()))
                    {
                        try
                        {
                            Directory.Delete(node.Tag.ToString());
                            node.Remove();
                            return fails;
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            MessageBox.Show(e.Message);
                            return ++fails;
                        }
                        catch (IOException e)
                        {
                            MessageBox.Show(e.Message);
                            return ++fails;
                        }
                    }
                }

            }
            else
            {
                if (node.Tag != null && Directory.Exists(node.Tag.ToString()) && node.Nodes.Count == 1 
                    && node.Nodes[0].Text.Equals("DUMMY"))
                {
                    AddChildren(node);
                }
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    if (node.Nodes[i].Tag == null)
                    {
                        fails++;
                        continue;
                    }
                    string path = node.Nodes[i].Tag.ToString();
                    if (File.Exists(path))
                    {
                        try
                        {
                            File.Delete(path);
                            node.Nodes[i].Remove();
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            MessageBox.Show(e.Message);
                            ++fails;
                        }
                        catch (IOException e)
                        {
                            MessageBox.Show(e.Message);
                            ++fails;
                        }
                    }
                    else if (Directory.Exists(path))
                    {
                        TreeNode child = node.Nodes[i];

                        if (child.Nodes.Count == 1 && child.Nodes[0].Text.Equals("DUMMY"))
                        {
                            AddChildren(child);
                        }

                        fails += RecursiveDelete(child);
                    }
                }
            }

            try
            {
                if (node.Tag == null) { return ++fails; }
                Directory.Delete(node.Tag.ToString());
                node.Remove();
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message);
                ++fails;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                ++fails;
            }

            return fails;
        }

        /// <summary>
        /// Searches one level of TreeNodes for matching file path
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="filePath"></param>
        /// <returns>TreeNode with Tag that matches given filepath</returns>
        public static TreeNode UpdateSelectedNode(TreeNodeCollection nodes, string filePath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag != null && node.Tag.ToString().Equals(filePath))
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// Recursively search all files and subdirectories of the given node for a node containing the matching path
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="path"></param>
        /// <returns>TreeNode with Tag that matches the given path</returns>
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
