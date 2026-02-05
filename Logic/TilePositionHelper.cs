using Microsoft.Xna.Framework;
using MonoGame_Game_Library.TileLogic;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Logic
{
    public static class TilePositionHelper
    {
        public static Vector2? GetTilePosition(int tileId, TileLayer layer, float layerScale = 1f)
        {
            for (int y = 0; y < layer.TileData.GetLength(0); y++)
            {
                for (int x = 0; x < layer.TileData.GetLength(1); x++)
                {
                    if (layer.TileData[y, x] == tileId)
                    {
                        // Convert grid position to world position
                        float scaledTileWidth = layer.TileWidth * layerScale;
                        float scaledTileHeight = layer.TileHeight * layerScale;

                        // Top-left corner of tile
                        return new Vector2(x * scaledTileWidth, y * scaledTileHeight);
                    }
                }
            }
            return null;
        }

        // To get center of tile instead of top-left:
        public static Vector2? GetTileCenterPosition(int tileId, TileLayer layer, float layerScale = 1f)
        {
            var position = GetTilePosition(tileId, layer, layerScale);
            if (position.HasValue)
            {
                float scaledTileWidth = layer.TileWidth * layerScale;
                float scaledTileHeight = layer.TileHeight * layerScale;

                return new Vector2(
                    position.Value.X + scaledTileWidth / 2,
                    position.Value.Y + scaledTileHeight / 2
                );
            }
            return null;
        }
    }
}
