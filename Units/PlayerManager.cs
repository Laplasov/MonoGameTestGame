using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Graphics;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Units
{
    public class PlayerManager : ITrackable
    {
        protected virtual string PlayerAtlasXML { set; get; } = "Atlases/CharacterAtlas.xml";

        protected virtual string PlayerName { set; get; } = "Player";

        private TextureAtlas _characterAtlas;
        private MovementController _movementController;
        private AnimationController _animationController;


        public List<UnitProfile> UnitList = new List<UnitProfile>();

        public Vector2 Position { get; set; }
        public bool LockPosition { get; set; } = false;
        public Vector2 Velocity => _movementController.Velocity;
        public float Speed => _movementController.Speed;
        public float Decay => _movementController.Decay;

        public PlayerManager() 
        {
        }
        public PlayerManager(Vector2 pos) => Position = new Vector2(pos.X, pos.Y);
        public PlayerManager WithPosition(Vector2 vec)
        {
            Position = vec;
            return this;
        }
        public void CreateUnits()
        {
            var playerUnit = new UnitProfile(PlayerName, new Vector2(1, 1));
            UnitList.Add(playerUnit);
        }
        public void Load(ContentManager Content)
        {
            _characterAtlas = TextureAtlas.FromFile(Content, PlayerAtlasXML);
            _movementController = new MovementController(InputHandel);
            _animationController = new AnimationController(_characterAtlas, angleOffset: -MathHelper.PiOver2);

            foreach (var unit in UnitList) 
            {
                unit.SetAtlas(_characterAtlas);
                unit.SetNewAnimationController();
            }

        }


        public void Update(GameTime gameTime)
        {
            if (LockPosition) 
                return;

            Vector2 movement = _movementController.Update(gameTime);
            Position += Velocity;
            _animationController.UpdateWorld(gameTime, Velocity, movement);
        }
        public void Draw() => _animationController.Draw(Core.SpriteBatch, Position);

        private Vector2 InputHandel()
        {
            Vector2 movement = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) movement.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) movement.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) movement.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) movement.Y += 1;
            if (movement.LengthSquared() > 1f)
                movement.Normalize();
            return movement;
        }
    }
}
