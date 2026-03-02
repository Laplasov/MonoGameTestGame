using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using Project1.Units.UnitProfilePlace;
using System.Collections.Generic;
using System.Linq;


namespace Project1.Scenes.BattleStates
{
    public class EnterBattleState : IBattleState
    {
        BattleScene _battleScene;

        public EnterBattleState(BattleScene battleScene)
        {
            _battleScene = battleScene;
            _battleScene.CameraManage.SetRotation(true);
        }

        public void Update(GameTime gameTime)
        {
            _battleScene.UIController.Update(gameTime);

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Space))
            {
                _battleScene.CameraManage.SetRotation(false);
                _battleScene.UIController.HideWelcome();
                Next();
            }
        }

        public void Next() => _battleScene.State = new AllayBattleState(_battleScene); 
    }
}
