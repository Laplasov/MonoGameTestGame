using Microsoft.Xna.Framework;
using System;

namespace Project1.Units
{
    public class MovementController
    {
        private Vector2 _velocity = Vector2.Zero;

        public Vector2 Velocity => _velocity;
        public float Speed { get; set; } = 10f;
        public float Decay { get; set; } = 6f;
        public bool LockPosition { get; set; } = false;

        // Delegate for getting movement input
        public Func<Vector2> GetMovementInput { get; set; }

        public MovementController(Func<Vector2> movementInputGetter)
        {
            GetMovementInput = movementInputGetter ?? throw new ArgumentNullException(nameof(movementInputGetter));
        }

        public Vector2 Update(GameTime gameTime)
        {
            if (LockPosition) return _velocity;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 movement = GetMovementInput?.Invoke() ?? Vector2.Zero;

            // Handle X axis
            if (movement.X != 0)
            {
                _velocity.X += movement.X * deltaTime * Speed;
            }
            else
            {
                _velocity.X *= (1f - deltaTime * Decay);
                if (Math.Abs(_velocity.X) < 1f) _velocity.X = 0f;
            }

            // Handle Y axis
            if (movement.Y != 0)
            {
                _velocity.Y += movement.Y * deltaTime * Speed;
            }
            else
            {
                _velocity.Y *= (1f - deltaTime * Decay);
                if (Math.Abs(_velocity.Y) < 1f) _velocity.Y = 0f;
            }

            // Clamp total speed
            if (_velocity.LengthSquared() > Speed * Speed)
            {
                _velocity = Vector2.Normalize(_velocity) * Speed;
            }

            return movement;
        }

        public void Reset()
        {
            _velocity = Vector2.Zero;
        }
    }
}