using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.TileLogic;
using Project1.Units.Visuals;

namespace Project1.Units
{
    public class UnitProfile
    {
        public BattleUnitView UnitView { get; private set; }
        AnimationController _animationController;
        TextureAtlas _textureAtlas;
        public string Name { get; set; } = "Unit";
        public UnitStats Stats { get; set; }
        public UnitAbilities Abilities { get; set; }

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
        public UnitProfile(string name, Vector2 position, UnitStats stats, UnitAbilities abilities)
        {
            Name = name;
            Position = position;
            Stats = stats;
            Abilities = abilities;
        }
        public void SetAnimation(TextureAtlas textureAtlas, float angleOffset)
        {
            _textureAtlas = textureAtlas;
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
