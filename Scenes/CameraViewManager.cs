using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.TileLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library.Scenes;
using Project1.Units;


namespace Project1.Scenes
{
    public class CameraViewManager
    {
        private bool _isRotating = true;
        private CameraMatrix3D _camera;
        Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        Vector3 Target { get; set; } = new Vector3(0, 0, 0);

        private float _currentCameraAngle;
        private float _cameraAngle = 3.8f;
        private float _orbitRadius = 5;
        private float _orbitHeight = 5;

        public float CurrentCameraAngle => _currentCameraAngle;
        public CameraMatrix3D Camera => _camera;
        public CameraViewManager()
        {
            _camera = new CameraMatrix3D(Core.GraphicsDevice);
            _camera.SetLookAt(Position, Target, Vector3.Up);
        }
        public void Update(TileMapLayered tileMap)
        {
            //Toggle rotation on R
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
                ToggleRotation();

            //Add to rotation
            if (_isRotating) _currentCameraAngle += 0.01f;
            else _currentCameraAngle = _cameraAngle;

            //Add center of view to camera
            var center = tileMap.GetPixelToWorldCenterScaled();
            _camera.OrbitAround(center: center, radius: _orbitRadius, angle: _currentCameraAngle, height: _orbitHeight);
        }

        private void ToggleRotation()
        {
            if (_isRotating)
                _currentCameraAngle = _cameraAngle;
            _isRotating = !_isRotating;

            if (_currentCameraAngle > MathHelper.TwoPi)
                _currentCameraAngle -= MathHelper.TwoPi;
        }
    }
}
