using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Scenes;
using Project1.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes
{
    internal class WorldTestScene : Scene
    {
        CameraMatrix _cameraMatrix;
        TileMapLayered _tileMapGround;
        Texture2D _tilesetTextureGround;
        EffectsManager _fogEffect;
        PlayerManager _playerManager;
        float _time;


        public override void Initialize()
        {
            Core.ExitOnEscape = true;
            _cameraMatrix = new CameraMatrix(Core.Graphics);

            _tileMapGround = TileMapLayered.LoadFromXml("Content/Tiles/TestTileMap1.xml");
            _fogEffect = new EffectsManager(Content, "Effects/FBM");

            _playerManager = new PlayerManager(400, 300);
            _cameraMatrix.TrackTarget(_playerManager);

            base.Initialize();
        }

        public override void LoadContent()
        {
            _tilesetTextureGround = Content.Load<Texture2D>("Images/TileMap");
            _tileMapGround.SetTilesetForAllLayers(_tilesetTextureGround, _tileMapGround.TileWidth, _tileMapGround.TileHeight);

            _playerManager.Load(Content, "Atlases/CharacterAtlas.xml");
        }

        public override void Update(GameTime gameTime)
        {
            _cameraMatrix.Update();
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _fogEffect.SetParameter("Time", _time);
            _playerManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

            Core.SpriteBatch.Begin(transformMatrix: _cameraMatrix.GetMatrix(), samplerState: SamplerState.PointClamp);

            _tileMapGround.DrawLayer(Core.SpriteBatch, "Ground", Vector2.Zero, 2);
            _playerManager.Draw();

            Core.SpriteBatch.End();

            _fogEffect.Draw();

        }
    }
}
