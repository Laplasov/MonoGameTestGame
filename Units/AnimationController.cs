using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library.Graphics;
using System;

namespace Project1.Units
{
    public class AnimationController
    {
        private AnimatedSprite _sprite;
        private TextureAtlas _atlas;
        private bool _standstill = true;
        private Direction _currentDirection = Direction.DOWN;
        private float _angleOffset = 0f;

        public Func<Direction, string> GetAnimationName { get; set; }
        public AnimatedSprite Sprite => _sprite;
        public Direction CurrentDirection => _currentDirection;
        public Vector2 Scale => _sprite.Scale;

        public AnimationController(TextureAtlas atlas, float size = 2, float angleOffset = 0f)
        {
            _angleOffset = angleOffset;
            _atlas = atlas;
            _sprite = _atlas.CreateAnimatedSprite(DirectionManager.InitialAnimation);
            _sprite.CenterOrigin();
            _sprite.Scale = new Vector2(size, size);
        }

        public void UpdateWorld(GameTime gameTime, Vector2 velocity, Vector2 movement)
        {
            if (_sprite == null) return;

            // Update based on velocity
            if (velocity != Vector2.Zero && movement != Vector2.Zero)
            {
                Animate(velocity);
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

        public void UpdateBattle(float angle)
        {
            Vector2 dirVec = DirectionManager.GetDirectionFromOrbitAngle(angle + _angleOffset);
            Animate(dirVec);
        }

        private void Animate(Vector2 dirVec)
        {
            Direction newDir = DirectionManager.GetDirectionFromVector(dirVec);

            if (_currentDirection != newDir)
            {
                _currentDirection = newDir;
                string animationName = DirectionManager.GetAnimationName(newDir);
                Animation newAnimation = _atlas.GetAnimation(animationName);
                _sprite.Animation = newAnimation;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) => _sprite?.Draw(spriteBatch, position);
        public void CenterOrigin() => _sprite?.CenterOrigin();
        public void ResetFrame() => _sprite?.ResetFrame();
        public Texture2D GetCurrentTexture() => _sprite?.GetCurrentTexture();
        public Rectangle GetCurrentSourceRect() => _sprite?.GetCurrentSourceRectangle() ?? Rectangle.Empty;
    }
}