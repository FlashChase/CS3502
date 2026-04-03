# CPU Scheduler Simulator

This application simulates and compares different CPU scheduling algorithms. 
Users can use different multiple scheduling methods and see statistics and comparisons of each.

## Platform Tested
- Windows 10 / Windows 11  
- Developed using Microsoft Visual Studio  
- Framework: .NET 8.0 (Windows)  
- UI Framework: Windows Forms  

## Build Instructions
1. Install Microsoft Visual Studio  
   - Ensure the “.NET Desktop Development” workload is installed  
2. Clone or download this repository  
3. Open the CpuScheduler.sln file in Visual Studio  
4. Build the solution:  
   - Press Ctrl + Shift + B or go to Build → Build Solution  
5. Run the application:  
   - Press F5 or click Start  

## Dependencies
- .NET 8.0 (Windows)  
- Windows Forms (included with .NET)  
- NuGet Packages:  
  - Microsoft.VisualBasic (v10.3.0)

## Features
- Implements multiple CPU scheduling algorithms:
  - First Come First Serve (FCFS)  
  - Shortest Job First (SJF)  
  - Shortest Remaining Time First (SRTF)  
  - Priority Scheduling  
  - Round Robin  
  - Highest Response Ratio Next (HRRN)  
- Displays performance metrics for comparison  

### License

This project is licensed under the terms of the [MIT license](LICENSE.txt).
