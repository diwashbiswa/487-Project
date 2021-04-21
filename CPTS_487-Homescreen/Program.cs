using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace CPTS_487_Homescreen
{
    public static class Program
    {
        static HomeScreen homeScreen;
        static Vector2 resolution = new Vector2(1280,720);
        static string exepath = "/CPTS_487-Peyton-Connor-Diwashi/bin/Debug/netcoreapp3.1/CPTS_487-Peyton-Connor-Diwashi.exe";

        [STAThread]
        static void Main()
        {
            Console.WriteLine("Path Found:  " + VisualStudioProvider.TryGetSolutionDirectoryInfo() + exepath);
            homeScreen = new HomeScreen();
            homeScreen.e_play += PlayButtonClicked;
            homeScreen.e_resolution += ResButtonClicked;
            homeScreen.Run();
        }

        static void PlayButtonClicked(object sender, System.EventArgs e)
        {
            ProcessStartInfo gproc = new ProcessStartInfo();
            gproc.Arguments = null;
            gproc.FileName = VisualStudioProvider.TryGetSolutionDirectoryInfo() + exepath;
            gproc.WindowStyle = ProcessWindowStyle.Normal;
            gproc.CreateNoWindow = true;
            using (Process proc = Process.Start(gproc))
            {
                proc.WaitForExit();
            }
        }

        static void ResButtonClicked(object sender, System.EventArgs e)
        {
            resolution = (Vector2)sender;
        }
    }

    // REFERENCE: https://stackoverflow.com/questions/19001423/getting-path-to-the-parent-folder-of-the-solution-file-using-c-sharp
    public static class VisualStudioProvider
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo( currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }
}
