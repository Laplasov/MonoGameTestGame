using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes.BattleStates
{
    public class AllayBattleState : IBattleState
    {
        BattleScene _battleScene;

        public AllayBattleState(BattleScene battleScene)
        {
            _battleScene = battleScene;
        }
        public void Next()
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
