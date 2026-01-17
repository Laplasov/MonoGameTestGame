using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class EffectsManager
    {
        public Effect Effect => _effect;

        Effect _effect;
        ContentManager _contentManager;
        Texture2D _whitePixel;
        BasicEffect _basicEffect;
        VertexPositionColorTexture[] _vertices;
        short[] _indices;
        Dictionary<string, dynamic> _parameters;

        public EffectsManager(ContentManager contentManager, string effectPath)
        {
            _effect = contentManager.Load<Effect>(effectPath);
            _contentManager = contentManager;
            _parameters = new Dictionary<string, dynamic>();
            _whitePixel = new Texture2D(Core.GraphicsDevice, 1, 1);
            _whitePixel.SetData(new[] { Color.White });
            Initialize();
        }

        public void SetEffect(string effectPath)
        {
            _effect?.Dispose();
            _effect = _contentManager.Load<Effect>(effectPath);
            ClearParameters();
        }

        void Initialize()
        {
            int w = Core.GraphicsDevice.Viewport.Width;
            int h = Core.GraphicsDevice.Viewport.Height;

            _vertices = new VertexPositionColorTexture[4];
            _vertices[0] = new VertexPositionColorTexture(new Vector3(0, 0, 0), Color.White, new Vector2(0, 0));
            _vertices[1] = new VertexPositionColorTexture(new Vector3(w, 0, 0), Color.White, new Vector2(1, 0));
            _vertices[2] = new VertexPositionColorTexture(new Vector3(0, h, 0), Color.White, new Vector2(0, 1));
            _vertices[3] = new VertexPositionColorTexture(new Vector3(w, h, 0), Color.White, new Vector2(1, 1));

            _indices = new short[] { 0, 1, 2, 1, 3, 2 };

            // Setup orthographic projection
            _basicEffect = new BasicEffect(Core.GraphicsDevice);
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.TextureEnabled = false;
            _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0, w, h, 0, 0, 1);
        }
        public dynamic GetParameter(string name) => _parameters.TryGetValue(name, out var value) ? value : null;
        public Dictionary<string, dynamic> GetParameters() => _parameters;
        public void RemoveParameter(string name) => _parameters.Remove(name);
        public void ClearParameters() => _parameters.Clear();
        public bool HasParameter(string name) => _effect.Parameters[name] != null;
        public void SetParameter(string name, dynamic value) => _parameters[name] = value;
        public void SetParameters(Dictionary<string, dynamic> parameters)
        {
            foreach (var kvp in parameters)
            {
                SetParameter(kvp.Key, kvp.Value);
            }
        }

        public void Draw(Texture2D texture = null, BlendState blendState = null)
        {
            if (blendState == null)
                blendState = BlendState.AlphaBlend;

            Core.GraphicsDevice.BlendState = blendState;

            ApplyParameters();

            // Set texture if provided
            if (texture != null && _effect.Parameters["SpriteTextureSampler"] != null)
                _effect.Parameters["SpriteTextureSampler"].SetValue(texture);

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Core.GraphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    _vertices,
                    0,
                    4,
                    _indices,
                    0,
                    2
                );
            }
        }
        void ApplyParameters()
        {
            // Always set matrix transform
            _effect.Parameters["MatrixTransform"]?.SetValue(_basicEffect.Projection);

            // Apply all stored parameters
            foreach (var kvp in _parameters)
            {
                _effect.Parameters[kvp.Key]?.SetValue(kvp.Value);
            }
        }
        public void OnViewportResized(int width, int height)
        {
            // Update vertices for new screen size
            _vertices[1].Position.X = width;
            _vertices[2].Position.Y = height;
            _vertices[3].Position = new Vector3(width, height, 0);

            // Update projection
            _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0, width, height, 0, 0, 1);
        }
    }
}
