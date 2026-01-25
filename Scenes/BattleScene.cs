using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Scenes;
using MonoGame_Game_Library.TileLogic;
using Project1.Units;
using System.Collections.Generic;

namespace Project1.Scenes
{
    public class BattleScene : Scene
    {
        protected virtual string MapXMLFile { set; get; } = "Content/Tiles/TestTileMapBattle1.xml";
        protected virtual string MapTexture { set; get; } = "Images/TileMap";
        protected virtual string Layer { set; get; } = "Ground";

        private TerrainRenderer _terrainRenderer;
        private TileMapLayered _tileMap;
        private CameraViewManager _cameraManage;
        private PlayerManager _player;
        private BattleUnitView _playerUnit;
        private List<UnitProfile> _units = new List<UnitProfile>();
        public BattleScene(PlayerManager player) => _player = player;

        public override void Initialize()
        {
            //Load tile map XML
            _tileMap = TileMapLayered.LoadFromXml(MapXMLFile);

            //Create camera
            _cameraManage = new CameraViewManager();

            base.Initialize();
        }

        public override void LoadContent()
        {
            //Load tile map image 
            var tileSetTexture = Content.Load<Texture2D>(MapTexture);
            _tileMap.SetTilesetForAllLayers(tileSetTexture, _tileMap.TileWidth, _tileMap.TileHeight);

            //Load to terrain with layer
            _terrainRenderer = new TerrainRenderer(Core.GraphicsDevice);
            _terrainRenderer.LoadFromTileMap(_tileMap, Layer);

            foreach (var unit in _player.UnitList)
            {
                unit.SetView(_tileMap);
                _units.Add(unit);
            }

        }

        public override void Update(GameTime gameTime)
        {
            _cameraManage.Update(_tileMap);

            foreach (var unit in _units)
                unit.Update(_cameraManage.CurrentCameraAngle);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw terrain
            _terrainRenderer.Draw(_cameraManage.Camera, Core.GraphicsDevice);

            //Draw all units
            foreach (var unit in _units)
                unit.Draw(_cameraManage.Camera, Core.GraphicsDevice);
        }

        void PlayerMovement()
        {
            bool moved = false;
            if (Core.Input.Keyboard.IsKeyDown(Keys.W))
            {_playerUnit.TilePosition += new Vector2(0, 0.1f);
                moved = true;}
            if (Core.Input.Keyboard.IsKeyDown(Keys.D))
            {_playerUnit.TilePosition += new Vector2(-0.1f, 0);
                moved = true;}
            if (Core.Input.Keyboard.IsKeyDown(Keys.A))
            {_playerUnit.TilePosition += new Vector2(0.1f, 0);
                moved = true;}
            if (Core.Input.Keyboard.IsKeyDown(Keys.S))
            {_playerUnit.TilePosition += new Vector2(0, -0.1f);
                moved = true;}

            if(moved)
                _playerUnit.UpdateWorldPosition(_tileMap);
        }
    }
}