using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.SpritLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //private Texture2D _textureEtra;

        private List<MovingSprite> _sprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here


            Texture2D _textureEtra = Content.Load<Texture2D>("Etra");
            _sprites = new List<MovingSprite>();

            for (int i = 0; i <= 10; i++)
            {
                _sprites.Add(new MovingSprite(_textureEtra, new Vector2(0, i + 10), i));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (MovingSprite sprite in _sprites) 
                sprite.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Debug.WriteLine("Space");

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                Debug.WriteLine("Mouse");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //_spriteBatch.Draw(_textureEtra, new Rectangle(100, 100, 100, 200), Color.White);
            foreach (MovingSprite sprite in _sprites)
                _spriteBatch.Draw(sprite.Texture, sprite.Rect, Color.White);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
