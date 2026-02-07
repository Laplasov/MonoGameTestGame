using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Graphics;
using Project1.Units.Visuals;
using System.Collections.Generic;

namespace Project1.Units
{
    public abstract class UnitBaseManager : ITrackable
    {
        protected virtual string PlayerAtlasXML { set; get; } = "Atlases/CharacterAtlas.xml";
        public virtual string PlayerName { set; get; } = "Player";

        protected TextureAtlas _characterAtlas;
        protected MovementController _movementController;
        protected AnimationController _animationController;

        public List<UnitProfile> UnitList = new List<UnitProfile>();

        public Vector2 Position { get; set; } = Vector2.Zero;
        public bool LockPosition { get; set; } = false;
        public Vector2 Velocity => _movementController.Velocity;


        private float _defaultSpeed = 10f; 
        public float Speed
        {
            get => _movementController?.Speed ?? _defaultSpeed;
            set
            {
                _defaultSpeed = value;
                if (_movementController != null)
                    _movementController.Speed = value;
            }
        }
        public float Decay => _movementController.Decay;

        protected virtual bool IsAlly { set; get; } = true;
        protected float AngleOffset => IsAlly ? -MathHelper.PiOver2 : MathHelper.PiOver2;
        protected Dictionary<int, Vector2> UnitPositions => IsAlly ? UnitPositionsAllay : UnitPositionsEnemy;

        protected static Dictionary<int, Vector2> UnitPositionsAllay = new Dictionary<int, Vector2>() 
        {
            { 1, new Vector2(1, 2) },{ 2, new Vector2(2, 2) },{ 3, new Vector2(3, 2) },
            { 4, new Vector2(1, 1) },{ 5, new Vector2(2, 1) },{ 6, new Vector2(3, 1) },
        };

        protected static Dictionary<int, Vector2> UnitPositionsEnemy = new Dictionary<int, Vector2>()
        {
            { 1, new Vector2(1, 5) },{ 2, new Vector2(2, 5) },{ 3, new Vector2(3, 5) },
            { 4, new Vector2(1, 6) },{ 5, new Vector2(2, 6) },{ 6, new Vector2(3, 6) },
        };

        public abstract void CreateUnits();
        protected abstract Vector2 InputHandel(GameTime gameTime);
        public virtual void Load(ContentManager Content)
        {
            _characterAtlas = TextureAtlas.FromFile(Content, PlayerAtlasXML);
            _movementController = new MovementController(InputHandel);
            _animationController = new AnimationController(_characterAtlas, angleOffset: AngleOffset);

            foreach (var unit in UnitList)
            {
                unit.SetAnimation(_characterAtlas, AngleOffset);
            }

        }
        public virtual void Update(GameTime gameTime)
        {
            if (LockPosition)
                return;

            Vector2 movement = _movementController.Update(gameTime);
            Position += Velocity;
            _animationController.UpdateWorld(gameTime, Velocity, movement);
        }
        public virtual void Draw() => _animationController.Draw(Core.SpriteBatch, Position);
        public Rectangle GetCurrentSourceRect() => _animationController?.GetCurrentSourceRect() ?? Rectangle.Empty;
        public Vector2 GetSpriteScale() => _animationController?.Scale ?? Vector2.One;

        public void ResetMovement() => _movementController.Reset();
    }
}

