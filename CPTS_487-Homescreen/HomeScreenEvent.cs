using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CPTS_487_Homescreen
{
    public partial class HomeScreen : Game
    {
        public event EventHandler e_exit = delegate { };
        public event EventHandler e_play = delegate { };
        public event EventHandler e_resolution = delegate { };

        /// <summary>
        /// On exitButton Click event, invoke 'e_exit' event from the home window. (Program.cs subscribes, exit all windows)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, System.EventArgs e)
        {
            e_exit.Invoke(this, new EventArgs());
            Exit();
        }

        /// <summary>
        /// On playButton Click event, invoke 'e_play' event from the home window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, System.EventArgs e)
        {
            e_play.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// On resButton Click event, scale GUI accordingly
        /// Invokes e_resolution event, sender is a Vector2 of new current screen resolution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resButton_Click(object sender, System.EventArgs e)
        {
            GUIButton b = (GUIButton)sender;
            if (b.Text == "1920 x 1080")
            {
                this.scaleGUI(1080.0f / 720.0f);
                this.setBackBufferResolution(1080, 1920);
                b.Text = "1024 x 576";
                this.currentWindowResolution.X = 1920;
                this.currentWindowResolution.Y = 1080;
            }
            else if (b.Text == "1280 x 720")
            {
                this.scaleGUI(720.0f / 576.0f);
                this.setBackBufferResolution(720, 1280);
                b.Text = "1920 x 1080";
                this.currentWindowResolution.X = 1280;
                this.currentWindowResolution.Y = 720;
            }
            else if (b.Text == "1024 x 576")
            {
                this.scaleGUI(576.0f / 1080.0f);
                this.setBackBufferResolution(576, 1024);
                b.Text = "1280 x 720";
                this.currentWindowResolution.X = 1024;
                this.currentWindowResolution.Y = 576;
            }
            e_resolution.Invoke(this.currentWindowResolution, new EventArgs());
        }

        /// <summary>
        /// Scale the entire GUI by a factor of n
        /// </summary>
        /// <param name="n"> scale factor </param>
        private void scaleGUI(float n)
        {
            foreach (GUIComponent gc in this._gui)
            {
                gc.Scale(n);
            }
        }

        /// <summary>
        /// Changes the resolution of this window
        /// </summary>
        /// <param name="h"> height </param>
        /// <param name="w"> width </param>
        private void setBackBufferResolution(int h, int w)
        {
            this._graphics.PreferredBackBufferHeight = h;
            this._graphics.PreferredBackBufferWidth = w;
            this._graphics.ApplyChanges();
        }
    }
}