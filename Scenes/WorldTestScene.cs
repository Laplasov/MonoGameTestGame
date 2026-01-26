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
        BattleScene _battleScene;
        float _time;

        public bool IsInBattle { get; set; } = false;
        public bool IsPaused { get; set; } = false;

        protected PlayerManager PlayerManager { get; set; }

        protected const string ShaderParamTimeName = "time";
        protected virtual string MapXMLFile { set; get; } = "Content/Tiles/TestTileMap1.xml";
        protected virtual string MapTexture { set; get; } = "Images/TileMap";
        protected virtual string Layer { set; get; } = "Ground";
        protected virtual string PlayerAtlasXML { set; get; } = "Atlases/CharacterAtlas.xml";
        protected virtual string EffectsPath { set; get; } = "Effects/FBM";
        protected virtual float LayerScale { set; get; } = 2;

        public WorldTestScene(PlayerManager playerManager) => PlayerManager = playerManager;
        public override void LoadContent()
        {
            //Visuals
            _cameraMatrix = new CameraMatrix(Core.Graphics);
            _fogEffect = new EffectsManager(Content, EffectsPath);
            _tileMapGround = TileMapLayered.LoadFromXml(MapXMLFile);

            _cameraMatrix.TrackTarget(PlayerManager);

            //Set texture for ground
            var tileSetTextureGround = Content.Load<Texture2D>(MapTexture);
            _tileMapGround.SetTilesetForAllLayers(tileSetTextureGround, _tileMapGround.TileWidth, _tileMapGround.TileHeight);

            PlayerManager.Load(Content);

            _battleScene = new BattleScene(PlayerManager);
            _battleScene.Initialize();
            _battleScene.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E)) IsInBattle = !IsInBattle;

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.T))
            {
                var playerManager = PlayerManager
                    .WithPosition(new Vector2(400, 300));
                //playerManager.UnitList.Clear();
                Core.ChangeScene(new WorldTestScene(playerManager));
                return;
            }

            if (IsInBattle)
            {
                _battleScene.Update(gameTime);
                return;
            }

            if (IsPaused) 
                return;

            _cameraMatrix.Update();

            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _fogEffect.SetParameter(ShaderParamTimeName, _time);
            PlayerManager.Update(gameTime);
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
            PlayerManager.Draw();

            Core.SpriteBatch.End();

            _fogEffect.Draw();

        }
    }
}
