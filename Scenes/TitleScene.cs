using Gum.Forms;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Scenes;
using MonoGameGum.Forms;
using Project1.Components.Castom;
using Project1.Save;
using Project1.UI;
using Project1.Units;
using System;
using System.Collections.Generic;
using static RenderingLibrary.Graphics.XMLFont;
using Button = Gum.Forms.Controls.Button;
using Label = Gum.Forms.Controls.Label;
using ListBox = Gum.Forms.Controls.ListBox;
using ListBoxItem = Gum.Forms.Controls.ListBoxItem;

namespace Project1.Scenes
{
    public class TitleScene : Scene
    {
        public override string SceneName { get; set; } = "TitleScene";
        EffectsManager _fogEffect;
        float _time;

        GraphicalUiElement _mainWindow;
        GraphicalUiElement _settingsWindow;
        GraphicalUiElement _loadsWindow;
        GraphicalUiElement _newGameWindow;

        SaveManager _saveManager;

        public override void Initialize()
        {
            Core.ExitOnEscape = true;
            _saveManager = new SaveManager();
            GameCore.SetScreenGum("MainMenu");
            InitializeUIGum();
            base.Initialize();
        }
        public override void LoadContent() => _fogEffect = new EffectsManager(Content, "Effects/FBM");
        public override void Update(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _fogEffect.SetParameter("Time", _time);
        }
        public override void Draw(GameTime gameTime) => _fogEffect.Draw();

        public void InitializeUIGum()
        {
            var ui = GameCore.GumElement;
            ui.BindButton("NewGameButton", ShowNewGameWindow);
            ui.BindButton("ExitButton", ExitGame);
            ui.BindButton("SettingsButton", SettingsWindow);
            ui.BindButton("LoadButton", LoadsWindow);
            ui.BindButton("ExitSettings", ShowMain);
            ui.BindButton("ExitLoad", ShowMain);
            ui.BindButton("ExitNewGameButton", ExitNewGameWindow);
            ui.BindButton("OkNewGameButton", AddNewSave); 
            ui.BindButton("DeleteAllLoad", DeleteAllSaves);
            ui.BindButton("DeleteLoad", DeleteSaves);
            ui.BindButton("LoadCurrent", LoadGame);

            _mainWindow = ui.GetGraphicalUiElementByName("MainWindow");
            _settingsWindow = ui.GetGraphicalUiElementByName("SettingsWindow");
            _loadsWindow = ui.GetGraphicalUiElementByName("LoadWindow");
            _newGameWindow = ui.GetGraphicalUiElementByName("NewGame");

            _saveManager.PopulateListBoxFromXML();

        }

        void AddNewSave(object sender, EventArgs e)
        {
            var newSave = _saveManager.AddNewSaveFromTextField();
            if(newSave != null)
                NewGame(newSave);
        }

        void DeleteAllSaves(object sender, EventArgs e) => _saveManager.DeleteAllSaves();
        void DeleteSaves(object sender, EventArgs e) => _saveManager.DeleteSelectedSave();
        void NewGame(SaveGame newSave)
        {
            var playerManager = new PlayerManager()
                .WithPosition(newSave.SceneData.Position)
                .WithName(newSave.SaveName);
            playerManager.CreateUnits();
            Core.ChangeScene(new WorldScene(playerManager, newSave.SceneData));
            GameCore.UnloadCurrentUI();
        }
        void LoadGame(object sender, EventArgs e) => LoadGame();
        void LoadGame()
        {
            SaveGame load = _saveManager.GetSelectedSaveData();
            if(load == null) return;

            var playerManager = new PlayerManager()
                .WithPosition(load.SceneData.Position);
            playerManager.CreateUnits();
            Core.ChangeScene(new WorldScene(playerManager, load.SceneData));
            GameCore.UnloadCurrentUI();
        }


        void ExitGame(object sender, EventArgs e) => GameCore.ExitCore();
        void SettingsWindow(object sender, EventArgs e)
        {
            _mainWindow.Visible = false;
            _settingsWindow.Visible = true;
        }
        void LoadsWindow(object sender, EventArgs e)
        {
            _mainWindow.Visible = false;
            _loadsWindow.Visible = true;
        }
        void ShowMain(object sender, EventArgs e)
        {
            _mainWindow.Visible = true;
            _settingsWindow.Visible = false;
            _loadsWindow.Visible = false;
        }
        void ShowNewGameWindow(object sender, EventArgs e)
        {
            _mainWindow.Visible = false;
            _newGameWindow.Visible = true;
        }
        void ExitNewGameWindow(object sender, EventArgs e)
        {
            _mainWindow.Visible = true;
            _newGameWindow.Visible = false;
        }

    }
}