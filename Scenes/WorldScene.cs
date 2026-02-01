using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Scenes;
using MonoGame_Game_Library.TileLogic;
using Project1.Save;
using Project1.Units;
using Project1.Logic;

namespace Project1.Scenes
{
    public class WorldScene : Scene
    {

        SceneData _sceneData;
        CameraMatrix _cameraMatrix;
        TileMapLayered _tileMapGround;
        EffectsManager _fogEffect;
        BattleScene _battleScene;
        float _time;

        protected const string ShaderParamTimeName = "time";
        protected const string CollisionLayer = "Collisions";
        protected const string EventLayer = "Events";

        public bool IsInBattle { get; set; } = false;
        public bool IsPaused { get; set; } = false;

        public override string SceneName { get; set; } = "WorldTestScene";
        public PlayerManager PlayerManager { get; set; }

        protected CollisionLogic _collisionLogic;
        protected TransitionHandler _transitionHandler;
        public SceneData SceneData => _sceneData;

        public WorldScene(PlayerManager playerManager, SceneData sceneData)
        {
            PlayerManager = playerManager;
            _sceneData = sceneData;
            SceneName = sceneData.SceneName;
        }

        public override void LoadContent()
        {
            //Visuals
            _cameraMatrix = new CameraMatrix(Core.Graphics);
            _fogEffect = new EffectsManager(Content, _sceneData.EffectsPath);
            _tileMapGround = TileMapLayered.LoadFromXml(_sceneData.MapXMLFile);

            //Looking on player
            _cameraMatrix.TrackTarget(PlayerManager);

            //Set texture for ground
            var tileSetTextureGround = Content.Load<Texture2D>(_sceneData.MapTexture);
            _tileMapGround.SetTilesetForAllLayers(tileSetTextureGround, _tileMapGround.TileWidth, _tileMapGround.TileHeight);

            //Set player for scene
            PlayerManager.Load(Content);

            //Create battle instanse need to be changed, events need to create them
            _battleScene = new BattleScene(PlayerManager);
            _battleScene.Initialize();
            _battleScene.LoadContent();

            //Set collision
            _collisionLogic = new CollisionLogic(PlayerManager, _tileMapGround.Layers[CollisionLayer], SceneData.LayerScale);

            //Set transition handler
            _transitionHandler = new TransitionHandler(PlayerManager, _tileMapGround.Layers[EventLayer], this, SceneData.LayerScale);
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E)) IsInBattle = !IsInBattle;

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.F4))
            {
                SaveManager.SaveGame(PlayerManager, _sceneData);
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

            _collisionLogic.CheckCollision();
            _transitionHandler.CheckTransition();
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsInBattle)
            {
                _battleScene.Draw(gameTime);
                return;
            }

            Core.SpriteBatch.Begin(transformMatrix: _cameraMatrix.GetMatrix(), samplerState: SamplerState.PointClamp);

            _tileMapGround.DrawLayer(Core.SpriteBatch, _sceneData.GroundLayer, Vector2.Zero, _sceneData.LayerScale);
            PlayerManager.Draw();

            Core.SpriteBatch.End();

            _fogEffect.Draw();

        }
    }
}
