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
    public enum Direction { UP, UP_LEFT, LEFT, DOWN_LEFT, DOWN, DOWN_RIGHT, RIGHT, UP_RIGHT}
    public class PlayerManager : ITrackable
    {
        TextureAtlas _characterAtlas;
        AnimatedSprite _playerSprite;
        Vector2 _lastPosition;
        Vector2 _velocity = Vector2.Zero;
        float Decay = 6f;
        bool _standstill = true;
        public Vector2 Position { get; set; }
        public float Speed { get; set; } = 10f;
        public bool LockPosition { get; set; } = false;
        public Direction Direction { get; set; } = Direction.DOWN;

        public PlayerManager(float x, float y) => Position = new Vector2(x, y);
        public void Load(ContentManager Content, string atlas)
        {
            _characterAtlas = TextureAtlas.FromFile(Content, atlas);
            _playerSprite = _characterAtlas.CreateAnimatedSprite("down");
            _playerSprite.CenterOrigin();
            _playerSprite.Scale = new Vector2(2, 2);
        }

        public void Update(GameTime gameTime)
        {
            _lastPosition = Position;
            if (LockPosition) return;

            Vector2 movement = HandelVelocity(gameTime);
            Position += _velocity;

            if (_velocity != Vector2.Zero && movement != Vector2.Zero)
            {
                Direction newDir = GetDirectionFromVector(_velocity);
                if (Direction != newDir)
                {
                    Direction = newDir;
                    string dirName = GetAnimationName(newDir);
                    Animation newAnimation = _characterAtlas.GetAnimation(dirName);
                    _playerSprite.Animation = newAnimation; 
                }
            }
            if (_velocity != Vector2.Zero)
            {
                _standstill = false;
                _playerSprite.Update(gameTime);
            }
            else if (!_standstill)
            {
                _standstill = true;
                _playerSprite.ResetFrame();
            }

        }

        private Vector2 HandelVelocity(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 movement = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.A)) movement.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) movement.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) movement.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) movement.Y += 1;
            if (movement.LengthSquared() > 1f)
                movement.Normalize();

            if (movement.X != 0)
            {
                // Add to X velocity
                _velocity.X += movement.X * deltaTime * Speed;
            }
            else
            {
                // Decay X velocity
                _velocity.X *= (1f - deltaTime * Decay);
                if (Math.Abs(_velocity.X) < 1f)
                {
                    _velocity.X = 0f;
                }
            }
            if (movement.Y != 0)
            {
                // Add to Y velocity
                _velocity.Y += movement.Y * deltaTime * Speed;
            }
            else
            {
                // Decay Y velocity
                _velocity.Y *= (1f - deltaTime * Decay);
                if (Math.Abs(_velocity.Y) < 1f)
                {
                    _velocity.Y = 0f;
                }
            }
            if (_velocity.LengthSquared() > Speed * Speed)
            {
                _velocity = Vector2.Normalize(_velocity) * Speed;
            }
            return movement;
        }

        private Direction GetDirectionFromVector(Vector2 vec)
        {
            // Determine direction based on X and Y
            if (vec.Y < 0)      // Moving up
            {
                if (vec.X > 0) return Direction.UP_LEFT;
                if (vec.X < 0) return Direction.UP_RIGHT;
                return Direction.UP;
            }
            else if (vec.Y > 0) // Moving down
            {
                if (vec.X < 0) return Direction.DOWN_RIGHT;
                if (vec.X > 0) return Direction.DOWN_LEFT;
                return Direction.DOWN;
            }
            else                // Moving horizontally
            {
                return vec.X < 0 ? Direction.RIGHT : Direction.LEFT;
            }
        }

        private string GetAnimationName(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return "up";
                case Direction.DOWN:
                    return "down";
                case Direction.LEFT:
                    return "left";
                case Direction.RIGHT:
                    return "right";
                case Direction.UP_LEFT:
                    return "up_left";
                case Direction.UP_RIGHT:
                    return "up_right";
                case Direction.DOWN_LEFT:
                    return "down_left";
                case Direction.DOWN_RIGHT:
                    return "down_right";
                default:
                    return "down";
            }
        }

        public void Draw()
        {
            _playerSprite.Draw(Core.SpriteBatch, Position);
        }

    }
}
