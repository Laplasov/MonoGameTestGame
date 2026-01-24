using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project1.Units
{
    public enum Direction { UP, UP_LEFT, LEFT, DOWN_LEFT, DOWN, DOWN_RIGHT, RIGHT, UP_RIGHT }
    public static class DirectionManager
    {
        public static float AngleOffset { get; set; } = 0f;

        public static string InitialAnimation = "down";

        public static string GetAnimationName(Direction direction)
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
        public static Vector2 GetDirectionFromOrbitAngle(float angle)
        {

            float normalized = (angle + AngleOffset) % MathHelper.TwoPi;
            if (normalized < 0) normalized += MathHelper.TwoPi;

            float degrees = MathHelper.ToDegrees(normalized);

            // Map angle to 8-direction vectors
            if (degrees >= 337.5f || degrees < 22.5f) return new Vector2(0, 1);      // DOWN
            if (degrees >= 22.5f && degrees < 67.5f) return new Vector2(1, 1);      // DOWN_RIGHT
            if (degrees >= 67.5f && degrees < 112.5f) return new Vector2(1, 0);      // RIGHT
            if (degrees >= 112.5f && degrees < 157.5f) return new Vector2(1, -1);     // UP_RIGHT
            if (degrees >= 157.5f && degrees < 202.5f) return new Vector2(0, -1);     // UP
            if (degrees >= 202.5f && degrees < 247.5f) return new Vector2(-1, -1);    // UP_LEFT
            if (degrees >= 247.5f && degrees < 292.5f) return new Vector2(-1, 0);     // LEFT
            return new Vector2(-1, 1); // DOWN_LEFT (292.5°-337.5°)
        }

        public static Direction GetDirectionFromVector(Vector2 vec)
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
    }
}
