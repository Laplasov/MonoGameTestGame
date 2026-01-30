using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using Project1.Save;
using Project1.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Scenes
{
    public class WorldTestScene2 : WorldTestScene
    {
        public WorldTestScene2(PlayerManager playerManager, SceneData sceneData) : base(playerManager, sceneData)
        {
            SceneName = "WorldTestScene2";
            sceneData.MapXMLFile = "Content/Tiles/TestTileMap2.xml";
            sceneData.MapTexture = "Images/TileMap";
            sceneData.Layer = "Ground";
            sceneData.EffectsPath = "Effects/FBM";
            sceneData.LayerScale = 1;
        }
        public override void Update(GameTime gameTime)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.F4))
            {
                SaveManager.SaveGame(PlayerManager, SceneData);
            }

            base.Update(gameTime);
        }

    }
}
