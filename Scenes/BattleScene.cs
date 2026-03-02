using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Scenes;
using MonoGame_Game_Library.TileLogic;
using Project1.Scenes.BattleStates;
using Project1.Scenes.CumulativeSystem;
using Project1.Units;
using Project1.Units.UnitProfilePlace;
using Project1.Units.Visuals;
using System.Collections.Generic;

namespace Project1.Scenes
{
    public class BattleScene : Scene
    {
        protected virtual string MapXMLFile { set; get; } = "Content/Tiles/TestTileMapBattle1.xml";
        protected virtual string MapTexture { set; get; } = "Images/TileMap";
        protected virtual string Layer { set; get; } = "Ground";
        public override string SceneName { get; set; } = "BattleScene";
        protected virtual string ScreenUI { set; get; } = "BattleMenu";

        private TerrainRenderer _terrainRenderer;
        private TileMapLayered _tileMap;
        private PlayerManager _player;
        private List<UnitProfile> _units = new List<UnitProfile>();

        public CameraViewManager CameraManage;
        public BattleUIController UIController;
        public CumulativeTurnSystem TurnSystem;

        public IBattleState State;

        public BattleScene(PlayerManager player) => _player = player;
        public override void LoadContent()
        {
            //Load tile map XML
            _tileMap = TileMapLayered.LoadFromXml(MapXMLFile);

            //Create camera
            CameraManage = new CameraViewManager();

            //Load tile map image 
            var tileSetTexture = Content.Load<Texture2D>(MapTexture);
            _tileMap.SetTilesetForAllLayers(tileSetTexture, _tileMap.TileWidth, _tileMap.TileHeight);

            //Load to terrain with layer
            _terrainRenderer = new TerrainRenderer(Core.GraphicsDevice);
            _terrainRenderer.LoadFromTileMap(_tileMap, Layer);

            UIController = new BattleUIController(CameraManage, _units);
            TurnSystem = new CumulativeTurnSystem(_units);

            State = new EnterBattleState(this);
        }
        public void SetEnemies(List<UnitProfile> enemy)
        {
            _units.Clear();

            foreach (var unit in _player.UnitList)
            {
                unit.SetView(_tileMap);
                _units.Add(unit);
            }
            foreach (var unit in enemy)
            {
                unit.SetView(_tileMap);
                _units.Add(unit);
            }

        }
        public override void Update(GameTime gameTime)
        {
            CameraManage.Update(_tileMap);

            State.Update(gameTime);

            foreach (var unit in _units)
                unit.Update(CameraManage.CurrentCameraAngle);
        }

        public void ResolveUI() => UIController.Resolve();
        public void InitializeUI() => UIController.Initialize(ScreenUI);

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw terrain
            _terrainRenderer.Draw(CameraManage.Camera, Core.GraphicsDevice);

            //Draw all units
            foreach (var unit in _units)
                unit.Draw(CameraManage.Camera, Core.GraphicsDevice);

        }
    }
}