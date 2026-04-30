File System Implementation Project

Overview
This program is a Windows Forms file manager built in C# using .NET.It allows users to navigate the file system, view file metadata, create files and folders,read and edit text files, search directories, and delete files or folders.

Features
- Navigate files and directories using a TreeView
- View file information such as name, path, size, and creation date
- Read text file contents
- Edit and save text files
- Create new files
- Create new folders
- Delete files and directories
- Search for files and folders
- Handles common file system errors with user messages

Technologies Used
- C#
- Windows Forms
- .NET
- System.IO library
- Visual Studio 2022

How to Run
1. Open the project folder in Visual Studio 2022.
2. Open the solution file (.sln).
3. Build the project.
4. Run the program using the Start button in Visual Studio.

Usage
When the program opens, the current directory is loaded into the TreeView.
Select files or folders in the TreeView to view information about them.
Double-click a text file to display its contents. 
Use the buttons to create, edit, save, search, or delete files and folders.

Notes
- The program uses lazy loading for directories to improve performance.
- Some non-text files may not display correctly if opened as text.
- Some files or folders may not be accessible due to operating system permissions. Most notable are folders in the current working directory.
- Large directory searches may take time and can make the program appear temporarily unresponsive.

Author
Chase Connell

Course
CS 3502 Operating Systems

Project
Project 3: File System Implementation