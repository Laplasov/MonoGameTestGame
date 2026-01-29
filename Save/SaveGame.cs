using Project1.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Save
{
    public class SaveGame
    {
        public string SaveName { get; set; }
        public string Location { get; set; }
        public DateTime SaveTime { get; set; }
        public string QualifiedName { get; set; }
        public SceneData SceneData { get; set; }

    }
    public class SceneData()
    {
        public string SceneName { get; set; } = "WorldTestScene";
        public string MapXMLFile { set; get; } = "Content/Tiles/TestTileMap1.xml";
        public string MapTexture { set; get; } = "Images/TileMap";
        public string Layer { set; get; } = "Ground";
        public string EffectsPath { set; get; } = "Effects/FBM";
        public float LayerScale { set; get; } = 2;
    }

    public class SaveGameList
    {
        public List<SaveGame> Items { get; set; } = new List<SaveGame>();
    }
}
