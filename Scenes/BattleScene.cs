using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Input;
using MonoGame_Game_Library.Scenes;
using MonoGame_Game_Library.TileLogic;
using Project1.Units;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;

namespace Project1.Scenes
{
    public class BattleScene : Scene
    {
        private CameraMatrix3D _camera;
        private TerrainRenderer _terrainRenderer;
        private TileMapLayered _tileMap;
        private float _currentCameraAngle;
        private float _cameraAngle = 3.8f;
        private bool _isRotating = true;
        private float _orbitRadius = 5;
        private float _orbitHeight = 5;

        Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        Vector3 Target { get; set; } = new Vector3(0, 0, 0);

        private PlayerManager _player;
        private BattleUnit _playerUnit;
        private List<BattleUnit> _units = new List<BattleUnit>();

        protected virtual string MapXMLFile { set; get; } = "Content/Tiles/TestTileMapBattle1.xml";
        protected virtual string MapTexture { set; get; } = "Images/TileMap";
        protected virtual string Layer { set; get; } = "Ground";

        public void SetPlayer(PlayerManager player) => _player = player;
        public override void Initialize()
        {
            Core.ExitOnEscape = true;
            _tileMap = TileMapLayered.LoadFromXml(MapXMLFile);

            _camera = new CameraMatrix3D(Core.GraphicsDevice);
            _camera.SetLookAt(Position, Target, Vector3.Up);

            base.Initialize();
        }

        public override void LoadContent()
        {
            var tileSetTexture = Content.Load<Texture2D>(MapTexture);
            _tileMap.SetTilesetForAllLayers(tileSetTexture, _tileMap.TileWidth, _tileMap.TileHeight);

            _terrainRenderer = new TerrainRenderer(Core.GraphicsDevice);
            _terrainRenderer.LoadFromTileMap(_tileMap, Layer);

            _playerUnit = new BattleUnit(Core.GraphicsDevice, new Vector2(1, 1), "Unit1");
            _playerUnit.UpdateWorldPosition(_tileMap);
            _units.Add(_playerUnit);

        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
                ToggleRotation();

            if (_isRotating)
                _currentCameraAngle += 0.01f;
            else
                _currentCameraAngle = _cameraAngle;

            var center = _tileMap.GetPixelToWorldCenterScaled();
            _camera.OrbitAround(center: center, radius: _orbitRadius, angle: _currentCameraAngle, height: _orbitHeight);

            var texture = _player.GetCurrentTexture();
            var sourceRect = _player.GetCurrentSourceRect();
            if (texture != null)
            {
                foreach (var unit in _units)
                    unit.UpdateTexture(texture, sourceRect);
            }

            _player.UpdatePerspective(gameTime, _currentCameraAngle);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.CornflowerBlue);
            _terrainRenderer.Draw(_camera, Core.GraphicsDevice);

            foreach (var unit in _units)
                unit.Sprite.Draw(_camera, Core.GraphicsDevice);
        }
        
        private void ToggleRotation()
        {
            if (_isRotating)
                _currentCameraAngle = _cameraAngle;
            _isRotating = !_isRotating;

            if (_currentCameraAngle > MathHelper.TwoPi)
                _currentCameraAngle -= MathHelper.TwoPi;
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