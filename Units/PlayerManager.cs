using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MonoGame_Game_Library.Camera;

namespace Project1.Units
{
    public class PlayerManager : ITrackable
    {
        TextureAtlas _characterAtlas;
        private MovementController _movementController;
        private AnimationController _animationController;

        public Vector2 Position { get; set; }
        public bool LockPosition { get; set; } = false;
        public Vector2 Velocity => _movementController.Velocity;
        public float Speed => _movementController.Speed;
        public float Decay => _movementController.Decay;

        public PlayerManager(Vector2 pos) => Position = new Vector2(pos.X, pos.Y);
        public void Load(ContentManager Content, string atlas)
        {
            _movementController = new MovementController(InputHandel);
            _characterAtlas = TextureAtlas.FromFile(Content, atlas);
            _animationController = new AnimationController(_characterAtlas, angleOffset: -MathHelper.PiOver2);
        }

        public void Update(GameTime gameTime)
        {
            if (LockPosition) 
                return;

            Vector2 movement = _movementController.Update(gameTime);
            Position += Velocity;
            _animationController.UpdateWorld(gameTime, Velocity, movement);
        }
        public void UpdatePerspective(GameTime gameTime, float angle) => _animationController.UpdateBattle(angle);
        public void Draw() => _animationController.Draw(Core.SpriteBatch, Position);
        public Texture2D GetCurrentTexture() => _animationController?.GetCurrentTexture();
        public Rectangle GetCurrentSourceRect() => _animationController?.GetCurrentSourceRect() ?? Rectangle.Empty;

        private Vector2 InputHandel()
        {
            Vector2 movement = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) movement.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) movement.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) movement.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) movement.Y += 1;
            if (movement.LengthSquared() > 1f)
                movement.Normalize();
            return movement;
        }
    }
}
