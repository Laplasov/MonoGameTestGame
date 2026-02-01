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
        public SceneData SceneData { get; set; }
        public string PlayerAtlasXML { get; set; } = "Atlases/CharacterAtlas.xml";

    }

    public class SaveGameList
    {
        public List<SaveGame> Items { get; set; } = new List<SaveGame>();
    }
}
