using Gum.Forms;
using Gum.Wireframe;
using Microsoft.Xna.Framework.Content;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Scenes;
using MonoGameGum.Forms;
using Project1.Components.Castom;
using Project1.Scenes;
using Project1.Units;
using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Serialization;
using ListBox = Gum.Forms.Controls.ListBox;
using TextBox = Gum.Forms.Controls.TextBox;

namespace Project1.Save
{
    public class SaveManager
    {
        private const string SaveDir = "Save";
        private const string SaveFileName = "SaveFile.xml";
        private const string ListBoxName = "ListBoxInstance";
        private const string TextFieldName = "TextBoxCastomInstance";
        private const string FrontSprite = "down_1";
        private const string TimeFormat = "yy.MM.dd HH:mm";
        private const string ScenesXML = "ScenesXML";
        private const string BaseSceneXML = "BaseScene.xml";

        public void PopulateListBoxFromXML()
        {
            string xmlPath = System.IO.Path.Combine(GameCore.Content.RootDirectory, SaveDir, SaveFileName);
            using (var stream = System.IO.File.OpenRead(xmlPath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                var saveList = (SaveGameList)serializer.Deserialize(stream);

                var _listBox = GameCore.GumElement.GetFrameworkElementByName<ListBox>(ListBoxName);
                _listBox.Items.Clear();

                foreach (var save in saveList.Items)
                {
                    var characterAtlas = TextureAtlas.FromFile(GameCore.Content, save.PlayerAtlasXML);
                    var listBoxItem = new ListBoxItemNewCastom();
                    TextureRegion region = characterAtlas.GetRegion(FrontSprite);

                    listBoxItem.SpriteInstance.Texture = region.Texture;
                    listBoxItem.SpriteInstance.SourceRectangle = region.SourceRectangle;

                    _listBox.Items.Add(listBoxItem);
                    listBoxItem.ListItemDisplayText = $"{save.SaveName}\n{save.Location}\n{save.SaveTime.ToString(TimeFormat)}";
                }
            }
        }

        public SaveGame AddNewSaveFromTextField()
        {
            var textField = GameCore.GumElement.GetFrameworkElementByName<TextBox>(TextFieldName);

            SaveGameList saveList;
            string saveName = textField.Text.Trim();

            string savePath = System.IO.Path.Combine(GameCore.Content.RootDirectory, SaveDir, SaveFileName);

            using (var stream = System.IO.File.OpenRead(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                saveList = (SaveGameList)serializer.Deserialize(stream);
            }

            foreach(SaveGame item in saveList.Items)
            {
                if (item.SaveName == textField.Text)
                    return null;
            }

            if (textField == null || string.IsNullOrWhiteSpace(textField.Text)) return null;

            var newSceneInstant = LoadSceneXML(BaseSceneXML);
            var newSave = new SaveGame
            {
                SaveName = saveName,
                Location = newSceneInstant.SceneName,
                SaveTime = System.DateTime.Now,
                SceneData = newSceneInstant
            };

            saveList.Items.Add(newSave);

            using (var stream = System.IO.File.Create(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                serializer.Serialize(stream, saveList);
            }

            textField.Text = "";

            var _listBox = GameCore.GumElement.GetFrameworkElementByName<ListBox>(ListBoxName);
            _listBox.Items.Clear();

            foreach (var save in saveList.Items)
            {
                var listBoxItem = new ListBoxItemNewCastom();
                _listBox.Items.Add(listBoxItem);
                listBoxItem.ListItemDisplayText = $"{save.SaveName}\n{save.Location}\n{save.SaveTime.ToString("yy.MM.dd HH:mm")}";
            }

            System.Diagnostics.Debug.WriteLine(savePath);

            return newSave;
        }

        public void DeleteAllSaves()
        {
            string savePath = System.IO.Path.Combine(GameCore.Content.RootDirectory, SaveDir, SaveFileName);

            if (!System.IO.File.Exists(savePath))
            {
                System.Diagnostics.Debug.WriteLine("No save file to delete.");
                return;
            }

            var emptySaveList = new SaveGameList
            {
                Items = new System.Collections.Generic.List<SaveGame>()
            };

            using (var stream = System.IO.File.Create(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                serializer.Serialize(stream, emptySaveList);
            }

            var _listBox = GameCore.GumElement.GetFrameworkElementByName<ListBox>(ListBoxName);
            _listBox.Items.Clear();

        }

        public void DeleteSelectedSave()
        {
            var _listBox = GameCore.GumElement.GetFrameworkElementByName<ListBox>(ListBoxName);

            // Check if any item is selected
            if (_listBox.SelectedIndex < 0)
            {
                System.Diagnostics.Debug.WriteLine("No item selected!");
                return;
            }

            int selectedIndex = _listBox.SelectedIndex;

            // Load current saves
            string savePath = System.IO.Path.Combine(GameCore.Content.RootDirectory, SaveDir, SaveFileName);

            if (!System.IO.File.Exists(savePath))
                return;

            SaveGameList saveList;
            using (var stream = System.IO.File.OpenRead(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                saveList = (SaveGameList)serializer.Deserialize(stream);
            }

            // Check if index is valid
            if (selectedIndex >= 0 && selectedIndex < saveList.Items.Count)
            {
                var deletedSave = saveList.Items[selectedIndex];

                // Remove the selected item
                saveList.Items.RemoveAt(selectedIndex);

                // Save back to XML
                using (var stream = System.IO.File.Create(savePath))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                    serializer.Serialize(stream, saveList);
                }

                // Refresh UI
                PopulateListBoxFromXML();

            }
        }

        public SaveGame GetSelectedSaveData()
        {
            var _listBox = GameCore.GumElement.GetFrameworkElementByName<ListBox>(ListBoxName);

            int selectedIndex = _listBox.SelectedIndex;

            string savePath = System.IO.Path.Combine(GameCore.Content.RootDirectory, SaveDir, SaveFileName);

            if (!System.IO.File.Exists(savePath))
            {
                System.Diagnostics.Debug.WriteLine("Save file doesn't exist!");
                return null;
            }

            SaveGameList saveList;
            using (var stream = System.IO.File.OpenRead(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                saveList = (SaveGameList)serializer.Deserialize(stream);
            }
            if (selectedIndex >= saveList.Items.Count || selectedIndex < 0)
            {
                return null;
            }

            return saveList.Items[selectedIndex];

        }

        public static void SaveGame(PlayerManager playerManager, SceneData sceneInstant)
        {
            SaveGameList saveList;

            string savePath = System.IO.Path.Combine(GameCore.Content.RootDirectory, SaveDir, SaveFileName);

            using (var stream = System.IO.File.OpenRead(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                saveList = (SaveGameList)serializer.Deserialize(stream);
            }

            SaveGame existingSave = saveList.Items.FirstOrDefault(item => item.SaveName == playerManager.PlayerName);

            if (existingSave != null)
            {
                existingSave.Location = sceneInstant.SceneName;
                existingSave.SaveTime = DateTime.Now;
                sceneInstant.Position = playerManager.Position;
                existingSave.SceneData = sceneInstant;
            }
            else return;

            using (var stream = System.IO.File.Create(savePath))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SaveGameList));
                serializer.Serialize(stream, saveList);
            }
        }
        public static SceneData LoadSceneXML(string SceneName)
        {
            string scenePath = System.IO.Path.Combine(GameCore.Content.RootDirectory, ScenesXML, SceneName);

            using (var stream = System.IO.File.OpenRead(scenePath))
            {
                var serializer = new XmlSerializer(typeof(SceneData));
                var sceneData = (SceneData)serializer.Deserialize(stream);
                return sceneData;
            }
        }
    }
}
