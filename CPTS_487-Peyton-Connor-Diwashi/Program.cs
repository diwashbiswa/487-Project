using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Starts the program by showing a home/options screen
    /// Game is stated via the 'e_play' event invoked from the home screen.
    /// </summary>
    public static class Program
    {
        static Vector2 resolution;

        static MainGame game;

        [STAThread]
        static void Main()
        {
            resolution = new Vector2(1280, 720);

            game = new MainGame();
            game.currentWindowResolution = resolution;
            game.Run();

        }
    }
}
