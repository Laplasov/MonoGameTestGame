using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Scenes;
using MonoGame_Game_Library.TileLogic;
using Project1.Save;
using Project1.Scenes;
using Project1.Units;
using Project1.Units.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Logic
{
    public class TransitionHandler
    {
        private PlayerManager _playerManager;
        private TileLayer _tileLayer;
        private WorldScene _scene;
        private Vector2 Position => _playerManager.Position;
        private int _tileWidth;
        private int _tileHeight;
        private float _layerScale;

        private const string EventLayerName = "Events";
        private const string DOTXML = ".xml";

        public TransitionHandler(PlayerManager playerManager, TileLayer tileLayer, WorldScene scene, float layerScale = 1f)
        {
            _playerManager = playerManager;
            _tileLayer = tileLayer;
            _tileWidth = tileLayer.TileWidth;
            _tileHeight = tileLayer.TileHeight;
            _layerScale = layerScale;
            _scene = scene;
        }

        public Rectangle GetPlayerBounds()
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

        public void CheckTransition()
        {

            Rectangle playerBounds = GetPlayerBounds();
            int scaledTileWidth = (int)(_tileWidth * _layerScale);
            int scaledTileHeight = (int)(_tileHeight * _layerScale);

            // Get overlapping tiles
            int leftTile = playerBounds.Left / scaledTileWidth;
            int rightTile = playerBounds.Right / scaledTileWidth;
            int topTile = playerBounds.Top / scaledTileHeight;
            int bottomTile = playerBounds.Bottom / scaledTileHeight;

            // Clamp to map bounds
            int mapWidth = _tileLayer.TileData.GetLength(1);
            int mapHeight = _tileLayer.TileData.GetLength(0);

            leftTile = Math.Max(0, leftTile);
            rightTile = Math.Min(mapWidth - 1, rightTile);
            topTile = Math.Max(0, topTile);
            bottomTile = Math.Min(mapHeight - 1, bottomTile);

            foreach (var transition in _scene.SceneData.Transitions)
            {
                // Find any tile with matching transition TileId
                for (int y = topTile; y <= bottomTile; y++)
                {
                    for (int x = leftTile; x <= rightTile; x++)
                    {
                        int currentTileId = _tileLayer.TileData[y, x];

                        if (currentTileId == transition.TileId)
                        {
                            Rectangle tileRect = new Rectangle(
                                x * scaledTileWidth,
                                y * scaledTileHeight,
                                scaledTileWidth,
                                scaledTileHeight
                            );

                            if (playerBounds.Intersects(tileRect) && Core.Input.Keyboard.WasKeyJustPressed(Keys.C))
                            {
                                var targetSceneData = SaveManager.LoadSceneXML(transition.TargetSceneName);

                                Vector2 returnPosition = GetWorldPositionForTileIdInScene(targetSceneData, _scene.SceneData.SceneName);

                                var playerManager = _scene.PlayerManager.WithPosition(returnPosition);
                                Core.ChangeScene(new WorldScene(playerManager, targetSceneData));
                                return;
                            }
                        }
                    }
                }
            }
        }

        public Vector2 GetWorldPositionForTileIdInScene(SceneData targetScene, string currentSceneName)
        {
            // Add .xml to match TargetSceneName format
            string expectedTargetName = currentSceneName + DOTXML;

            var returnTransition = targetScene.Transitions
                .FirstOrDefault(t => t.TargetSceneName == expectedTargetName);

            if (returnTransition != null)
            {
                var targetTileMap = TileMapLayered.LoadFromXml(targetScene.MapXMLFile);
                var targetEventsLayer = targetTileMap.Layers[EventLayerName];

                for (int y = 0; y < targetEventsLayer.TileData.GetLength(0); y++)
                {
                    for (int x = 0; x < targetEventsLayer.TileData.GetLength(1); x++)
                    {
                        if (targetEventsLayer.TileData[y, x] == returnTransition.TileId)
                        {
                            float scaledTileWidth = targetScene.LayerScale * targetTileMap.TileWidth;
                            float scaledTileHeight = targetScene.LayerScale * targetTileMap.TileHeight;

                            // Top-left corner of tile
                            Vector2 tileTopLeft = new Vector2(x * scaledTileWidth, y * scaledTileHeight);

                            // Center of tile
                            Vector2 tileCenter = new Vector2(
                                tileTopLeft.X + scaledTileWidth / 2,
                                tileTopLeft.Y + scaledTileHeight / 2
                            );

                            return tileCenter;
                        }
                    }
                }
            }

            return targetScene.Position;
        }
    }
}