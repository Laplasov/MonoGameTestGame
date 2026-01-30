using Project1.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Project1.Save
{
    public class SaveGame
    {
        public string SaveName { get; set; }
        public string Location { get; set; }
        public DateTime SaveTime { get; set; }
        public SceneData SceneData { get; set; }
        public string PlayerAtlasXML { get; set; } = "Atlases/CharacterAtlas.xml";

    }
    public class SceneData()
    {
        public string SceneName { get; set; } = "WorldTestScene";
        public string MapXMLFile { set; get; } = "Content/Tiles/TestTileMap1.xml";
        public string MapTexture { set; get; } = "Images/TileMap";
        public string Layer { set; get; } = "Ground";
        public string EffectsPath { set; get; } = "Effects/FBM";
        public float LayerScale { set; get; } = 2;
        public float PositionX { set; get; } = 800f;
        public float PositionY { set; get; } = 600f;
        [XmlIgnore]
        public Vector2 Position
        {
            get => new Vector2(PositionX, PositionY);
            set 
            {
                PositionX = value.X;
                PositionY = value.Y;
            }
        }
    }

    public class SaveGameList
    {
        public List<SaveGame> Items { get; set; } = new List<SaveGame>();
    }
}
