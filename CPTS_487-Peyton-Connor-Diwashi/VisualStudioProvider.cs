using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    // REFERENCE: https://stackoverflow.com/questions/19001423/getting-path-to-the-parent-folder-of-the-solution-file-using-c-sharp
    public static class VisualStudioProvider
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }
}
