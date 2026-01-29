using Gum.Forms;
using Gum.Wireframe;
using Microsoft.Xna.Framework.Content;
using MonoGame_Game_Library.Scenes;
using MonoGameGum.Forms;
using Project1.Components.Castom;
using Project1.Scenes;
using System;
using ListBox = Gum.Forms.Controls.ListBox;
using TextBox = Gum.Forms.Controls.TextBox;

namespace Project1.Save
{
    internal class SaveManager
    {
        private const string SaveDir = "Save";
        private const string SaveFileName = "SaveFile.xml";
        private const string ListBoxName = "ListBoxInstance";
        private const string TextFieldName = "TextBoxCastomInstance";

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
                    var listBoxItem = new ListBoxItemNewCastom();
                    _listBox.Items.Add(listBoxItem);
                    listBoxItem.ListItemDisplayText = $"{save.SaveName}\n{save.Location}\n{save.SaveTime.ToString("yy.MM.dd HH:mm")}";
                }
            }
        }

        public void AddNewSaveFromTextField(Scene currentScene)
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
                    return;
            }

            if (textField == null || string.IsNullOrWhiteSpace(textField.Text)) return;


            var newSave = new SaveGame
            {
                SaveName = saveName,
                Location = currentScene.SceneName,
                SaveTime = System.DateTime.Now,
                QualifiedName = currentScene.GetType().AssemblyQualifiedName
            };
            //Type sceneType = Type.GetType(fullName);
            //Scene scene = (Scene)Activator.CreateInstance(sceneType);

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
    }
}
