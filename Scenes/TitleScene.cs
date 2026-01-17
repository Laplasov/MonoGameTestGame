using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Scenes;
using System;
using System.Runtime.InteropServices;

namespace Project1.Scenes
{
    public class TitleScene : Scene
    {
        float _time;
        EffectsManager _fogEffect;

        public override void Initialize()
        {
            Core.ExitOnEscape = true;
            _fogEffect = new EffectsManager(Content, "Effects/FBM");

            base.Initialize();
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _fogEffect.SetParameter("Time", _time);

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Enter))
            {
                Core.ChangeScene(new WorldTestScene());
            }

        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);
            _fogEffect.Draw();
        }
    }
}