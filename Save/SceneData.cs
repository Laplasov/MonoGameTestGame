using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using Project1.Save.Bestiary;

namespace Project1.Save
{
    public class SceneData
    {
        public string SceneName { get; set; } = "WorldTestScene";
        public string MapXMLFile { set; get; } = "Content/Tiles/TestTileMap1.xml";
        public string MapTexture { set; get; } = "Images/TileMap";
        public string GroundLayer { set; get; } = "Ground";
        public string EffectsPath { set; get; } = "Effects/FBM";
        public float LayerScale { set; get; } = 2;
        public float PositionX { set; get; } = 800f;
        public float PositionY { set; get; } = 600f;
        public List<SceneTransition> Transitions { get; set; } = new List<SceneTransition>();

        [XmlArray("EnemySpawns")]
        [XmlArrayItem("EnemySpawn")]
        public List<EnemySpawn> EnemySpawns { get; set; } = new List<EnemySpawn>();

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
}