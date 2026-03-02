using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame_Game_Library;
using Project1.Save;
using Project1.Save.Abilities;
using Project1.Save.Bestiary;
using Project1.Units.UnitProfilePlace;
using System;

namespace Project1.Units.Managers
{
    /// <summary>
    /// Represents a single enemy encounter on the world map
    /// </summary>
    public class EnemyManager : UnitBaseManager
    {
        private PlayerManager _playerManager;
        private EnemySpawn _spawnData;
        private EnemyTemplate _template;
        private Vector2 _originalPosition;
        private SceneData _sceneData;
        private BestiaryLoader _bestiaryLoader;
        private AbilityLoader _abilityLoader;

        float PositionTolerance = 32f;
        private enum MovementState { Idle, Chase, Back }
        private MovementState _currentState = MovementState.Idle;

        public EnemyManager(PlayerManager playerManager, EnemySpawn spawnData, EnemyTemplate template, Vector2 position, SceneData sceneData, BestiaryLoader bestiaryLoader, AbilityLoader abilityLoader)
        {
            _playerManager = playerManager;
            _spawnData = spawnData;
            _template = template;
            _originalPosition = position;
            _sceneData = sceneData;
            _bestiaryLoader = bestiaryLoader;
            _abilityLoader = abilityLoader;

            // Set base class properties
            Speed = spawnData.Speed;
            Position = position;
            PlayerName = spawnData.EnemyName;
            PlayerAtlasXML = template.AtlasXML;
            IsAlly = false; // Enemies face opposite direction
            CreateUnits();
        }

        public override void CreateUnits()
        {
            Speed = _spawnData.Speed;
            UnitList.Clear();
            foreach (var spawnUnit in _spawnData.Units)
            {
                if (UnitPositions.TryGetValue(spawnUnit.Index, out Vector2 unitPosition))
                {
                    var loader = _bestiaryLoader.GetEnemyTemplate(spawnUnit.Name);
                    UnitStats stats = loader.Stats.Clone();
                    UnitAbilities abilities = _abilityLoader.GetAbilities(loader.Abilities);
                    var unit = new UnitProfile(spawnUnit.Name, unitPosition, stats, abilities, spawnUnit.Index, IsAlly);
                    UnitList.Add(unit);

                }
            }

        }

        protected override Vector2 InputHandel(GameTime gameTime)
        {
            // Calculate distances
            float distanceToPlayer = Vector2.Distance(_originalPosition, _playerManager.Position);
            float distanceToHome = Vector2.Distance(Position, _originalPosition);

            // Apply scale to ranges
            float scaledAggroRange = _spawnData.AggroRange * _sceneData.LayerScale;
            float scaledPositionTolerance = PositionTolerance * _sceneData.LayerScale;

            bool atOriginalPosition = distanceToHome < scaledPositionTolerance;

            MovementState newState;

            if (distanceToPlayer <= scaledAggroRange)
                newState = MovementState.Chase;
            else if (!atOriginalPosition)
                newState = MovementState.Back;
            else
                newState = MovementState.Idle;

            if (_currentState != newState)
            {
                _currentState = newState;
                ResetMovement();
            }

            switch (_currentState)
            {
                case MovementState.Chase:
                    Vector2 chaseDirection = _playerManager.Position - Position;
                    if (chaseDirection.LengthSquared() > 0)
                    {
                        chaseDirection.Normalize();
                        return chaseDirection;
                    }
                    break;

                case MovementState.Back:
                    Vector2 returnDirection = _originalPosition - Position;
                    if (returnDirection.LengthSquared() > 0)
                    {
                        returnDirection.Normalize();
                        return returnDirection;
                    }
                    break;

                case MovementState.Idle:
                    return Vector2.Zero;
            }

            return Vector2.Zero;
        }

        public Rectangle GetCollisionBounds()
        {
            int width = _template.CollisionBoxWidth;
            int height = _template.CollisionBoxHeight;

            return new Rectangle(
                (int)(Position.X - width / 2),
                (int)(Position.Y - height / 2),
                width,
                height
            );
        }

        public string GetMovementPattern() => _spawnData.MovementPattern;
    }
}