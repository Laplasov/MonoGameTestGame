using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Scenes;
using Project1.UI;
using System;

namespace Project1.Scenes
{
    public class TitleScene : Scene
    {
        EffectsManager _fogEffect;
        float _time;

        GraphicalUiElement _mainWindow;
        GraphicalUiElement _settingsWindow;
        GraphicalUiElement _loadsWindow;

        public override void Initialize()
        {
            Core.ExitOnEscape = true;
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
            ui.BindButton("NewGameButton", ChangeScene);
            ui.BindButton("ExitButton", ExitGame);
            ui.BindButton("SettingsButton", SettingsWindow);
            ui.BindButton("LoadButton", LoadsWindow);
            ui.BindButton("ExitSettings", ShowMain);
            ui.BindButton("ExitLoad", ShowMain);

            _mainWindow = ui.GetGraphicalUiElementByName("MainWindow");
            _settingsWindow = ui.GetGraphicalUiElementByName("SettingsWindow");
            _loadsWindow = ui.GetGraphicalUiElementByName("LoadWindow");
        }

        void ChangeScene(object sender, EventArgs e)
        {
            GameCore.UnloadCurrentUI();
            Core.ChangeScene(new WorldTestScene());
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

    }
}