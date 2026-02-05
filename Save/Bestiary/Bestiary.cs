using System.Collections.Generic;
using System.Xml.Serialization;

namespace Project1.Save.Bestiary
{
    public class Bestiary
    {
        [XmlElement("EnemyTemplate")]
        public List<EnemyTemplate> EnemyTemplates { get; set; } = new List<EnemyTemplate>();
    }
}
