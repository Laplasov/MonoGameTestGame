using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Project1.Save.Bestiary;
using System;

namespace Project1.Units
{
    /// <summary>
    /// Represents a single enemy encounter on the world map
    /// </summary>
    public class EnemyManager : UnitBaseManager
    {
        private PlayerManager _playerManager;
        private EnemySpawn _spawnData;
        private EnemyTemplate _template;

        public EnemyManager(PlayerManager playerManager, EnemySpawn spawnData, EnemyTemplate template, Vector2 position)
        {
            _playerManager = playerManager;
            _spawnData = spawnData;
            _template = template;

            // Set base class properties
            Position = position;
            PlayerName = spawnData.EnemyName;
            PlayerAtlasXML = template.AtlasXML;
            IsAlly = false; // Enemies face opposite direction
            CreateUnits();
        }

        public override void CreateUnits()
        {
            UnitList.Clear();
            foreach (var spawnUnit in _spawnData.Units)
            {
                if (UnitPositions.TryGetValue(spawnUnit.Index, out Vector2 unitPosition))
                {
                    var unit = new UnitProfile(spawnUnit.Name, unitPosition);
                    UnitList.Add(unit);

                }
            }

        }

        protected override Vector2 InputHandel(GameTime gameTime)
        {
            // Stationary enemies don't move
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