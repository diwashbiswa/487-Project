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

        [STAThread]
        static void Main()
        {
            homeScreen = new HomeScreen();
            homeScreen.e_exit += exitGame;
            homeScreen.e_play += playGame;
            homeScreen.e_resolution += resolutionUpdated;
            resolution = new Vector2(1280, 720);
            homeScreen.Run();

            return;
        }

        // Homescreen exit button clicked
        private static void exitGame(object sender, System.EventArgs e)
        {
            homeScreen.Exit();
        }

        // Homescreen play button clicked
        private static void playGame(object sender, System.EventArgs e)
        {
            // Exit the homescreen and start the game
            homeScreen.Exit();
            game = new MainGame();
            game.currentWindowResolution = resolution;
            game.Run();
        }

        // Resolution changed from the home screen
        private static void resolutionUpdated(object sender, System.EventArgs e)
        {
            resolution = (Vector2)sender;
        }
    }
}
