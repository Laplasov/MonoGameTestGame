using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library.Graphics;
using System;

namespace Project1.Units
{
    public enum Direction { UP, UP_LEFT, LEFT, DOWN_LEFT, DOWN, DOWN_RIGHT, RIGHT, UP_RIGHT }
    public class AnimationController
    {
        private AnimatedSprite _sprite;
        private TextureAtlas _atlas;
        private bool _standstill = true;
        private Direction _currentDirection = Direction.DOWN;

        public Func<Direction, string> GetAnimationName { get; set; }
        public AnimatedSprite Sprite => _sprite;
        public Direction CurrentDirection => _currentDirection;
        public Vector2 Scale => _sprite.Scale;

        public AnimationController(TextureAtlas atlas, string initialAnimation, Func<Direction, string> animationNameGetter, float size = 2)
        {
            _atlas = atlas 
                ?? throw new ArgumentNullException(nameof(atlas));

            GetAnimationName = animationNameGetter 
                ?? throw new ArgumentNullException(nameof(animationNameGetter));

            _sprite = _atlas.CreateAnimatedSprite(initialAnimation);
            _sprite.CenterOrigin();
            _sprite.Scale = new Vector2(size, size);
        }

        public void Update(GameTime gameTime, Vector2 velocity, Vector2 movement)
        {
            if (_sprite == null) return;

            // Update based on velocity
            if (velocity != Vector2.Zero && movement != Vector2.Zero) // Moving
            {
                Direction newDirection = GetDirectionFromVector(velocity);

                // Change animation if direction changed
                if (_currentDirection != newDirection)
                {
                    _currentDirection = newDirection;
                    string animationName = GetAnimationName?.Invoke(newDirection) ?? "down";
                    Animation newAnimation = _atlas.GetAnimation(animationName);
                    _sprite.Animation = newAnimation;
                }
            }
            if (velocity != Vector2.Zero)
            {
                _standstill = false;
                _sprite.Update(gameTime);
            }
            else if (!_standstill)
            {
                _standstill = true;
                _sprite.ResetFrame();
            }
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

        public void Draw(SpriteBatch spriteBatch, Vector2 position) => _sprite?.Draw(spriteBatch, position);
        public void CenterOrigin() => _sprite?.CenterOrigin();
        public void ResetFrame() => _sprite?.ResetFrame();
    }
}