using Project1.Units;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Project1.Save.Bestiary
{
    public class EnemyTemplate
    {
        public string Name { get; set; }
        public string AtlasXML { get; set; }
        public string MovementPattern { get; set; }
        public int CollisionBoxWidth { get; set; }
        public int CollisionBoxHeight { get; set; }

        [XmlElement("Stats")]
        public UnitStats Stats { get; set; }

        [XmlArray("Abilities")]
        [XmlArrayItem("Ability")]
        public string[] Abilities { get; set; } = new string[0];
    }
}
