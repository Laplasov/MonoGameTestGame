using Project1.Abilities;
using Project1.Save.Bestiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1.Save.Abilities
{
    public class Abilities
    {
        [XmlElement("Ability")]
        public List<Ability> Ability { get; set; } = new();
    }
}
