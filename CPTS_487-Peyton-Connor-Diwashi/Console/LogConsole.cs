using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public static class LogConsole
    {
        public static void Log(string s)
        {
            Console.WriteLine(s);
        }

        public static void LogPosition(string desc, int x, int y)
        {
            string line = desc + ": (";
            line += x.ToString();
            line += ",";
            line += y.ToString();
            line += ")";
            Console.WriteLine(line);
        }
    }
}
