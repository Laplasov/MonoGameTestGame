using System.Collections.Generic;
using System.Xml.Serialization;
namespace Project1.Save.Bestiary;
public class EnemySpawn
{
    [XmlAttribute("TileId")]
    public int TileId { get; set; }

    [XmlAttribute("EnemyName")]
    public string EnemyName { get; set; }

    [XmlAttribute("MovementPattern")]
    public string MovementPattern { get; set; }

    [XmlElement("Unit")]
    public List<EnemySpawnUnit> Units { get; set; } = new List<EnemySpawnUnit>();
}
