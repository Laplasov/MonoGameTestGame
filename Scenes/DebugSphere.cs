using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library.Graphics;
using System;

namespace Project1.Scenes
{
    public class DebugSphere
    {
        private VertexPositionColor[] _vertices;
        private short[] _indices;
        private BasicEffect _effect;
        private int _segments = 16;

        public Vector3 Position { get; set; }
        public float Radius { get; set; }
        public bool Visible { get; set; } = true;

        // Test parameters
        public float HeightOffset { get; set; } = 0.5f; // 0 = bottom, 0.5 = center, 1 = top

        public DebugSphere(GraphicsDevice device, float radius, Vector3 position)
        {
            Radius = radius;
            Position = position;
            CreateSphere(device);

            _effect = new BasicEffect(device)
            {
                VertexColorEnabled = true,
                LightingEnabled = false,
                Alpha = 0.5f
            };
        }

        private void CreateSphere(GraphicsDevice device)
        {
            int vertexCount = (_segments + 1) * (_segments + 1);
            _vertices = new VertexPositionColor[vertexCount];

            float phiStep = MathHelper.Pi / _segments;
            float thetaStep = MathHelper.TwoPi / _segments;

            int index = 0;
            for (int i = 0; i <= _segments; i++)
            {
                float phi = i * phiStep;
                for (int j = 0; j <= _segments; j++)
                {
                    float theta = j * thetaStep;

                    float x = (float)(Math.Sin(phi) * Math.Cos(theta));
                    float y = (float)(Math.Cos(phi));
                    float z = (float)(Math.Sin(phi) * Math.Sin(theta));

                    Vector3 pos = new Vector3(x, y, z) * Radius;
                    _vertices[index++] = new VertexPositionColor(pos, Color.Black);
                }
            }

            // Create indices
            _indices = new short[_segments * _segments * 6];
            index = 0;
            for (int i = 0; i < _segments; i++)
            {
                for (int j = 0; j < _segments; j++)
                {
                    int topLeft = i * (_segments + 1) + j;
                    int topRight = topLeft + 1;
                    int bottomLeft = (i + 1) * (_segments + 1) + j;
                    int bottomRight = bottomLeft + 1;

                    _indices[index++] = (short)topLeft;
                    _indices[index++] = (short)topRight;
                    _indices[index++] = (short)bottomLeft;

                    _indices[index++] = (short)topRight;
                    _indices[index++] = (short)bottomRight;
                    _indices[index++] = (short)bottomLeft;
                }
            }
        }

        public void Update(Vector3 spriteBase, Vector2 spriteSize)
        {
            // Calculate position based on offset
            Position = new Vector3(
                spriteBase.X,
                spriteBase.Y + (spriteSize.Y * HeightOffset),
                spriteBase.Z
            );
        }

        public bool CheckRayIntersection(Ray ray)
        {
            float? distance = ray.Intersects(new BoundingSphere(Position, Radius));
            return distance.HasValue;
        }

        public void Draw(CameraMatrix3D camera, GraphicsDevice device)
        {
            if (!Visible) return;

            // Set world matrix to position the sphere
            _effect.World = Matrix.CreateTranslation(Position);
            _effect.View = camera.View;
            _effect.Projection = camera.Projection;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    _vertices, 0, _vertices.Length,
                    _indices, 0, _indices.Length / 3
                );
            }
        }
    }
}