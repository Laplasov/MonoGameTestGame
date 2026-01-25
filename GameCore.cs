using Gum.DataTypes;
using Gum.Forms;
using Gum.Forms.Controls;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Camera;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Input;
using MonoGameGum;
using MonoGameGum.Forms.Controls;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using Project1.Gum;
using Project1.Scenes;
using Project1.SpritLogic;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using Button = Gum.Forms.Controls.Button;

namespace Project1;

public class GameCore : Core
{
    private static GameCore _instance;
    static GraphicalUiElement _graphicalUiElement;
    static List<Action> _eventCleanups = new List<Action>();
    public static GraphicalUiElement GumElement => _graphicalUiElement;
    static GumProjectSave GumProject { get; set; }
    GumService GumUI => GumService.Default;
    public GameCore() : base("GameTest", 1280, 720, false) => _instance = this;

    protected override void Initialize()
    {
        GumProject = GumUI.Initialize(this, "GumProject/GumProject.gumx");
        ChangeScene(new TitleScene());
        base.Initialize();
    }
    public static void ExitCore() => _instance.Exit();
    protected override void LoadContent() => base.LoadContent();
    protected override void UnloadContent()
    {
        _instance = null;
        UnloadCurrentUI();
        base.UnloadContent();
    }
    protected override void Update(GameTime gameTime)
    {
        if (_graphicalUiElement != null)
            GumService.Default.Update(gameTime, _graphicalUiElement);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        base.Draw(gameTime);
        GumService.Default.Draw();
    }

    public static GraphicalUiElement SetScreenGum(string screenName)
    {
        UnloadCurrentUI();

        //Set scene
        var screen = GumProject.Screens.Find(item => item.Name == screenName);
        _graphicalUiElement = screen.ToGraphicalUiElement(SystemManagers.Default);
        _graphicalUiElement.AddToManagers(SystemManagers.Default);

        return _graphicalUiElement;
    }
    public static void RegisterEventCleanup(Action cleanupAction) => _eventCleanups.Add(cleanupAction);
    public static void UnloadCurrentUI()
    {
        foreach (var cleanup in _eventCleanups)
        {
            cleanup?.Invoke();
        }
        _eventCleanups.Clear();

        if (_graphicalUiElement != null)
        {
            _graphicalUiElement.RemoveFromManagers();
            _graphicalUiElement = null;
        }
    }
}