using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.TileLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Scenes;
using Project1.Units;
using RenderingLibrary.Graphics;

namespace Project1.Units
{
    public class BattleUnit
    {
        public BillboardSprite Sprite { get; private set; }
        public Vector2 TilePosition { get; set; }
        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }

        private static float PIXEL_TO_WORLD_SCALE = 0.01f;

        public BattleUnit(GraphicsDevice device, Vector2 tilePos, string name)
        {
            TilePosition = tilePos;
            Name = name;
            Sprite = new BillboardSprite(device, Vector3.Zero, 0.5f, 0.5f);
        }

        public void UpdateWorldPosition(TileMapLayered tileMap)
        {
            Vector3 worldPos = tileMap.TileToWorld(
                (int)TilePosition.X,
                (int)TilePosition.Y,
                0.5f
            ) * PIXEL_TO_WORLD_SCALE;

            Sprite.Position = worldPos;
        }

        public void UpdateTexture(Texture2D texture, Rectangle sourceRect)
        {
            Texture = texture;
            SourceRect = sourceRect;
            Sprite.UpdateTexture(texture, sourceRect);
        }
    }
}
