using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes.BattleStates
{
    public interface IBattleState
    {
        public void Update(GameTime gameTime);
        public void Next();
    }
}
