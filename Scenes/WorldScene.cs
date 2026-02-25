using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Scenes;
using MonoGame_Game_Library.TileLogic;
using Project1.Save;
using Project1.Logic;
using Project1.Save.Bestiary;
using Project1.Units.Managers;
using Project1.Units;

namespace Project1.Scenes
{
    public class WorldScene : Scene
    {

        SceneData _sceneData;
        CameraMatrix _cameraMatrix;
        TileMapLayered _tileMap;
        EffectsManager _fogEffect;
        BattleScene _battleScene;
        float _time;

        protected const string ShaderParamTimeName = "time";
        protected const string CollisionLayer = "Collisions";
        protected const string EventLayer = "Events";
        protected const string SpawnLayer = "Spowns";
        protected const float EnemyDetectionRange = 32f;

        public bool IsInBattle { get; set; } = false;
        public bool IsPaused { get; set; } = false;

        public override string SceneName { get; set; } = "WorldTestScene";
        public PlayerManager PlayerManager { get; set; }

        protected CollisionLogic _collisionLogic;
        protected TransitionHandler _transitionHandler;
        protected EnemySceneCollection _enemyCollection;
        protected ProximityCollisionDetector _proximityDetector;

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
            _tileMap = TileMapLayered.LoadFromXml(_sceneData.MapXMLFile);

            //Looking on player
            _cameraMatrix.TrackTarget(PlayerManager);

            //Set texture for ground
            var tileSetTextureGround = Content.Load<Texture2D>(_sceneData.MapTexture);
            _tileMap.SetTilesetForAllLayers(tileSetTextureGround, _tileMap.TileWidth, _tileMap.TileHeight);

            //Set player for scene
            PlayerManager.Load(Content);

            //Create battle instanse need to be changed, events need to create them
            _battleScene = new BattleScene(PlayerManager);
            _battleScene.Initialize();
            _battleScene.LoadContent();

            //Set collision
            _collisionLogic = new CollisionLogic(PlayerManager, _tileMap.Layers[CollisionLayer], SceneData.LayerScale);

            //Set transition handler
            _transitionHandler = new TransitionHandler(PlayerManager, _tileMap.Layers[EventLayer], this, SceneData.LayerScale);

            //Set enemy events
            _enemyCollection = new EnemySceneCollection(PlayerManager, _tileMap.Layers[SpawnLayer], SceneData, Content);
            _enemyCollection.Load(Content);

            _proximityDetector = new ProximityCollisionDetector(PlayerManager, _enemyCollection, detectionRange: EnemyDetectionRange, layerScale: SceneData.LayerScale);
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E)) 
            {
                IsInBattle = !IsInBattle;
                _battleScene.ResolveUI();
            } 


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
            _enemyCollection.Update(gameTime);

            _collisionLogic.CheckCollision();
            _transitionHandler.CheckTransition();

            var nearbyEnemy = _proximityDetector.CheckProximity();
            if (nearbyEnemy != null)
            {
                _battleScene.InitializeUI();
                _battleScene.SetEnemies(nearbyEnemy.UnitList);
                IsInBattle = true;
                _enemyCollection.RemoveEnemy(nearbyEnemy);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsInBattle)
            {
                _battleScene.Draw(gameTime);
                return;
            }

            Core.SpriteBatch.Begin(transformMatrix: _cameraMatrix.GetMatrix(), samplerState: SamplerState.PointClamp);

            _tileMap.DrawLayer(Core.SpriteBatch, _sceneData.GroundLayer, Vector2.Zero, _sceneData.LayerScale);
            PlayerManager.Draw();
            _enemyCollection.Draw();

            Core.SpriteBatch.End();

            _fogEffect.Draw();

        }
    }
}
