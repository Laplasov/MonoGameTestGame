using Project1.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame_Game_Library.TileLogic;

namespace Project1.Logic
{
    public class CollisionLogic
    {
        private PlayerManager _playerManager;
        private TileLayer _tileLayer;
        private Vector2 Position => _playerManager.Position;
        private int _tileWidth;
        private int _tileHeight;
        private float _layerScale;
        private Vector2 _lastPosition;
        private const int SOLID_TILE_ID = 104;

        public CollisionLogic(PlayerManager playerManager, TileLayer tileLayer, float layerScale = 1f)
        {
            _playerManager = playerManager;
            _tileLayer = tileLayer;
            _tileWidth = tileLayer.TileWidth;
            _tileHeight = tileLayer.TileHeight;
            _lastPosition = playerManager.Position;
            _layerScale = layerScale;
        }

        public Rectangle GetCollisionBounds()
        {
            const int playerWidth = 32;
            const int playerHeight = 32;
            return new Rectangle(
                (int)(Position.X - playerWidth / 2),
                (int)(Position.Y - playerHeight / 2),
                playerWidth,
                playerHeight
            );
        }

        public void CheckCollision()
        {
            Vector2 targetPosition = Position; // Store the intended position
            Vector2 validPosition = _lastPosition; // Start with last valid position

            // Try X axis movement
            _playerManager.Position = new Vector2(targetPosition.X, _lastPosition.Y);
            if (!HasCollision())
            {
                // X movement is valid, keep it
                validPosition.X = targetPosition.X;
            }

            // Try Y axis movement (always test, regardless of X result)
            _playerManager.Position = new Vector2(validPosition.X, targetPosition.Y);
            if (!HasCollision())
            {
                // Y movement is valid, keep it
                validPosition.Y = targetPosition.Y;
            }

            // Apply the final valid position
            _playerManager.Position = validPosition;

            // Update last position
            _lastPosition = validPosition;
        }

        private bool HasCollision()
        {
            Rectangle playerBounds = GetCollisionBounds();

            // Calculate scaled tile dimensions
            int scaledTileWidth = (int)(_tileWidth * _layerScale);
            int scaledTileHeight = (int)(_tileHeight * _layerScale);

            // Calculate which tiles the player touches
            int leftTile = playerBounds.Left / scaledTileWidth;
            int rightTile = playerBounds.Right / scaledTileWidth;
            int topTile = playerBounds.Top / scaledTileHeight;
            int bottomTile = playerBounds.Bottom / scaledTileHeight;

            int mapWidth = _tileLayer.TileData.GetLength(1);
            int mapHeight = _tileLayer.TileData.GetLength(0);

            leftTile = Math.Max(0, leftTile);
            rightTile = Math.Min(mapWidth - 1, rightTile);
            topTile = Math.Max(0, topTile);
            bottomTile = Math.Min(mapHeight - 1, bottomTile);

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    // Check if tile is solid
                    if (_tileLayer.TileData[y, x] == SOLID_TILE_ID)
                    {
                        // Create tile rectangle
                        Rectangle tileRect = new Rectangle(
                            x * scaledTileWidth,
                            y * scaledTileHeight,
                            scaledTileWidth,
                            scaledTileHeight
                        );

                        // Check if player intersects this tile
                        if (playerBounds.Intersects(tileRect))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}