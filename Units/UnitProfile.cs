using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.TileLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Units
{
    public class UnitProfile
    {
        public BattleUnitView UnitView { get; private set; }
        AnimationController _animationController;
        TextureAtlas _textureAtlas;
        public string Name { get; set; } = "Unit1";

        private Vector2 _position = new Vector2(1, 1);
        public Vector2 Position { 
            get {
                if (UnitView == null)
                    return _position;
                else
                    return UnitView.TilePosition;
            } 
            set
            {
                if (UnitView == null)
                    _position = value;
                else
                {
                    UnitView.TilePosition = value;
                    _position = value;
                }
            }
        } 
        public UnitProfile(string name, Vector2 position, AnimationController animationController)
        {
            Name = name;
            Position = position;
            _animationController = animationController;
        }
        public UnitProfile(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }
        public void SetAtlas(TextureAtlas textureAtlas) => _textureAtlas = textureAtlas;
        public void SetNewAnimationController(float angleOffset = -MathHelper.PiOver2)
        {
            _animationController = new AnimationController(_textureAtlas, angleOffset: angleOffset);
        }
        public void SetView(TileMapLayered tileMap)
        {
            UnitView = new BattleUnitView(_position, _animationController, Core.GraphicsDevice);
            UnitView.UpdateWorldPosition(tileMap);
        }
        public void Update(float angle) => UnitView.Update(angle);
        public void Draw(CameraMatrix3D camera, GraphicsDevice device) => UnitView.Sprite.Draw(camera, device);
    }
}
