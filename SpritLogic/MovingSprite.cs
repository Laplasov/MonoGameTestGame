using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.SpritLogic
{
    internal class MovingSprite : ScaledSprit
    {
        private float _speed;
        public MovingSprite(Texture2D texture, Vector2 position, float speed) : base(texture, position)
        {
            _speed = speed;
        }

        public override void Update()
        {
            base.Update();
            Position.X += _speed;
        }
    }
}
