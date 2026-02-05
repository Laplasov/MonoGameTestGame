using System.Xml.Serialization;
namespace Project1.Save.Bestiary;
public class EnemySpawnUnit
{
    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlAttribute("Index")]
    public int Index { get; set; }
}