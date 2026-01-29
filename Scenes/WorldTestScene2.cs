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
        public override string SceneName { get; set; } = "WorldTestScene2";
        protected override string MapXMLFile { set; get; } = "Content/Tiles/TestTileMap2.xml";
        protected override string MapTexture { set; get; } = "Images/TileMap";
        protected override string Layer { set; get; } = "Ground";
        protected override string EffectsPath { set; get; } = "Effects/FBM";
        protected override float LayerScale { set; get; } = 1;
        public WorldTestScene2(PlayerManager playerManager) : base(playerManager){}

    }
}
