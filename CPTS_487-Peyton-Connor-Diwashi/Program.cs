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
        static HomeScreen homeScreen;
        static MainGame game;
        static Vector2 resolution;
        static bool hs_exit = true;

        [STAThread]
        static void Main()
        {
            resolution = new Vector2(1280, 720);

            while (hs_exit)
            {
                homeScreen = new HomeScreen();
                homeScreen.e_exit += HomescreenExit;
                homeScreen.e_play += HomeScreenPlay;
                homeScreen.e_resolution += HomeScreenResolution;
                homeScreen.Exiting += HomeScreenRestart;
                homeScreen.Run();
            }
        }

        // Homescreen exit button clicked (Final Exit)
        private static void HomescreenExit(object sender, System.EventArgs e)
        {
            hs_exit = false;
            homeScreen.Exit();
        }

        // Homescreen Exit with restart
        private static void HomeScreenRestart(object sender, System.EventArgs e)
        {
            homeScreen.Exit();
        }

        // Homescreen play button clicked
        private static void HomeScreenPlay(object sender, System.EventArgs e)
        {
            game = new MainGame();
            game.currentWindowResolution = resolution;
            game.Run();
        }

        // Resolution changed from the home screen
        private static void HomeScreenResolution(object sender, System.EventArgs e)
        {
            resolution = (Vector2)sender;
        }
    }
}
