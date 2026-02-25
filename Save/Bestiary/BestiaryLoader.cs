using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Project1.Save.Bestiary;

public class BestiaryLoader
{
    private const string BestiaryPath = "Bestiary/Bestiary.xml";
    private Bestiary _bestiary;

    public BestiaryLoader(ContentManager content)
    {
        LoadBestiary(content);
    }

    private void LoadBestiary(ContentManager content)
    {
        string path = Path.Combine(content.RootDirectory, BestiaryPath);

        using (var stream = File.OpenRead(path))
        {
            var serializer = new XmlSerializer(typeof(Bestiary));
            _bestiary = (Bestiary)serializer.Deserialize(stream);
        }
    }

    public EnemyTemplate GetEnemyTemplate(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        return _bestiary.EnemyTemplates.FirstOrDefault(e => e.Name == name);
    }
}