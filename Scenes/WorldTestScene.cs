using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Input;
using MonoGame_Game_Library.Scenes;
using MonoGameGum.Forms;
using Project1.Units;
using MonoGame_Game_Library.TileLogic;

namespace Project1.Scenes
{
    internal class WorldTestScene : Scene
    {

        CameraMatrix _cameraMatrix;
        TileMapLayered _tileMapGround;
        EffectsManager _fogEffect;
        PlayerManager _playerManager;
        BattleScene _battleScene;
        float _time;

        public bool IsInBattle { get; set; } = false;
        public bool IsPaused { get; set; } = false;

        protected const string ShaderParamTimeName = "time";
        protected virtual string MapXMLFile { set; get; } = "Content/Tiles/TestTileMap1.xml";
        protected virtual string MapTexture { set; get; } = "Images/TileMap";
        protected virtual string Layer { set; get; } = "Ground";
        protected virtual string PlayerAtlasXML { set; get; } = "Atlases/CharacterAtlas.xml";
        protected virtual string EffectsPath { set; get; } = "Effects/FBM";
        protected virtual Vector2 PlayerPosition { set; get; } = new Vector2(400, 300);
        protected virtual float LayerScale { set; get; } = 2;

        public override void Initialize()
        {
            //Visuals
            _cameraMatrix = new CameraMatrix(Core.Graphics);
            _tileMapGround = TileMapLayered.LoadFromXml(MapXMLFile);
            _fogEffect = new EffectsManager(Content, EffectsPath);


            // Set player and buttle right away, need to be changed for battle event later 
            _playerManager = new PlayerManager(PlayerPosition);
            _cameraMatrix.TrackTarget(_playerManager);

            _battleScene = new BattleScene();
            _battleScene.Initialize();
            _battleScene.SetPlayer(_playerManager);


            base.Initialize();
        }

        public override void LoadContent()
        {
            //Set texture for ground
            var tileSetTextureGround = Content.Load<Texture2D>(MapTexture);
            _tileMapGround.SetTilesetForAllLayers(tileSetTextureGround, _tileMapGround.TileWidth, _tileMapGround.TileHeight);

            _playerManager.Load(Content, PlayerAtlasXML);
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E)) IsInBattle = !IsInBattle;

            if (IsPaused) return;
            if (IsInBattle)
            {
                _battleScene.Update(gameTime);
                return;
            }
            _cameraMatrix.Update();

            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _fogEffect.SetParameter(ShaderParamTimeName, _time);
            _playerManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsInBattle)
            {
                _battleScene.Draw(gameTime);
                return;
            }

            Core.SpriteBatch.Begin(transformMatrix: _cameraMatrix.GetMatrix(), samplerState: SamplerState.PointClamp);

            _tileMapGround.DrawLayer(Core.SpriteBatch, Layer, Vector2.Zero, LayerScale);
            _playerManager.Draw();

            Core.SpriteBatch.End();

            _fogEffect.Draw();

        }
    }
}
