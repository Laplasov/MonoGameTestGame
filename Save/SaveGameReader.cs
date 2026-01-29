using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Save
{
    public class SaveGameReader : ContentTypeReader<SaveGame>
    {
        protected override SaveGame Read(ContentReader input, SaveGame existingInstance)
        {
            SaveGame saveGame = new SaveGame();
            saveGame.SaveName = input.ReadString();
            return saveGame;
        }
    }
}
