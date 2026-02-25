using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Project1.Abilities;
using Project1.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1.Save.Abilities
{
    public class AbilityLoader
    {
        private const string AbilityPath = "AbilitiesXML/Abilities.xml";
        private const int AbilityLimit = 4;
        private Abilities _abilities;
        public AbilityLoader(ContentManager content)
        {
            Load(content);
        }
        private void Load(ContentManager content)
        {
            string path = Path.Combine(content.RootDirectory, AbilityPath);

            using (var stream = File.OpenRead(path))
            {
                var serializer = new XmlSerializer(typeof(Abilities));
                _abilities = (Abilities)serializer.Deserialize(stream);
            }
        }

        public UnitAbilities GetAbilities(string[] names)
        {
            var unitAbilities = new UnitAbilities();

            if (names == null) return unitAbilities;

            int index = 0;
            foreach (var name in names)
            {
                if (index >= AbilityLimit) break; 
                if (!string.IsNullOrEmpty(name))
                {
                    var ability = GetAbility(name);
                    if (ability != null)
                        unitAbilities.Abilities[index++] = ability;
                }
            }
            return unitAbilities;
        }

        public Ability GetAbility(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return _abilities?.Ability?.FirstOrDefault(a => a.Name == name);
        }
    }

}
