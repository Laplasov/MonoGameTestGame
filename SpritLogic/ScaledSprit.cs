using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.SpritLogic
{
    internal class ScaledSprit : Sprit
    {
        public Rectangle Rect 
        { 
            get => new Rectangle((int)Position.X, (int)Position.Y, 100, 200); 
        }
        public ScaledSprit(Texture2D texture, Vector2 position) : base(texture, position) { }

    }
}
