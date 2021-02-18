using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

//
namespace CPTS_487_Peyton_Connor_Diwashi
{
    public partial class HomeScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 currentWindowResolution = new Vector2(1280, 720);

        // List of all componenets for the GUI on the home screen
        private List<GUIComponent> _gui = new List<GUIComponent>();

        public HomeScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // The GUI is first initialized to fit a 720p resolution, everything can then be scaled to any resolution
        protected override void Initialize()
        {
            // Play game button
            GUIButton button_playgame = new GUIButton(new Vector2(20, 600), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "Start");
            button_playgame.Precidence = 100;
            // Exit button
            GUIButton button_exit = new GUIButton(new Vector2(340, 600), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "Exit");
            button_exit.Precidence = 100;
            // Resolution Button
            GUIButton button_res = new GUIButton(new Vector2(960, 600), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "1920 x 1080");
            button_res.Precidence = 100;
            // Title screen
            GUIButton title = new GUIButton(new Vector2(0, 0), Content.Load<Texture2D>("Homescreen"), Content.Load<SpriteFont>("Font"), Color.Black, null);
            title.Precidence = 0;
            title.ShadeOnHover = false;
            // Call specific methods in HomeScreenEvent.cs when button events are Invoked
            button_exit.Click += this.exitButton_Click;
            button_playgame.Click += this.playButton_Click;
            button_res.Click += this.resButton_Click;
            // Add all GUIComponent types to a list of GUIComponents
            _gui.Add(button_playgame);
            _gui.Add(button_exit);
            _gui.Add(button_res);
            _gui.Add(title);
            // Sort GUIComponents based off of GUIComponenet.Precidence to determine which should be drawn first (lower is first)
            _gui.Sort((x, y) => x.Precidence.CompareTo(y.Precidence));
            // Set preferred resolution for the home screen
            _graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            _graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(GUIComponent gc in _gui)
            {
                gc.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            // Draw all GUI components
            foreach (GUIComponent gc in _gui)
            {
                gc.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
