using Microsoft.Xna.Framework;
using Project1.Units;
using System;

namespace Project1.Logic
{
    /// <summary>
    /// Handles proximity-based collision detection between player and enemies.
    /// Uses circular proximity from sprite feet positions.
    /// </summary>
    public class ProximityCollisionDetector
    {
        private PlayerManager _playerManager;
        private EnemySceneCollection _enemyCollection;
        private float _detectionRange;

        public float DetectionRange
        {
            get => _detectionRange;
            set => _detectionRange = value;
        }

        public ProximityCollisionDetector(PlayerManager playerManager, EnemySceneCollection enemyCollection, float detectionRange = 32f)
        {
            _playerManager = playerManager;
            _enemyCollection = enemyCollection;
            _detectionRange = detectionRange;
        }

        /// <summary>
        /// Check if player is within detection range of any enemy.
        /// Returns the closest enemy within range, or null if none found.
        /// </summary>
        public EnemyManager CheckProximity()
        {
            Vector2 playerFeet = GetFeetPosition(_playerManager);

            EnemyManager closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (var enemy in _enemyCollection.GetEnemies())
            {
                Vector2 enemyFeet = GetFeetPosition(enemy);

                // Calculate distance between feet positions
                float distance = Vector2.Distance(playerFeet, enemyFeet);

                // Check if within range
                if (distance <= _detectionRange)
                {
                    // Track the closest enemy
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            return closestEnemy;
        }

        private Vector2 GetFeetPosition(UnitBaseManager unit)
        {
            // Get the actual sprite source rectangle and scale
            var sourceRect = unit.GetCurrentSourceRect();
            var scale = unit.GetSpriteScale();

            // Calculate scaled sprite height
            float scaledHeight = sourceRect.Height * scale.Y;

            // Feet are at bottom of sprite (sprite origin is centered)
            float feetOffset = scaledHeight / 2f;

            return new Vector2(
                unit.Position.X,
                unit.Position.Y + feetOffset
            );
        }

        /// <summary>
        /// Optional: Get the distance to nearest enemy (for UI indicators, etc.)
        /// </summary>
        public float GetDistanceToNearestEnemy()
        {
            Vector2 playerFeet = GetFeetPosition(_playerManager);
            float nearestDistance = float.MaxValue;

            foreach (var enemy in _enemyCollection.GetEnemies())
            {
                Vector2 enemyFeet = GetFeetPosition(enemy);
                float distance = Vector2.Distance(playerFeet, enemyFeet);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                }
            }

            return nearestDistance == float.MaxValue ? -1f : nearestDistance;
        }
    }
}