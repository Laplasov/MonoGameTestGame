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
using System.Diagnostics;
using System.Reflection.Emit;
using Button = Gum.Forms.Controls.Button;

namespace Project1;

public class GameTest2 : Core
{

    Texture2D _textureEtra;
    CameraMatrix _cameraMatrix;

    TestShowSprite _testShowSprite;
    GraphicalUiElement _graphicalUiElement;
    GumService GumUI => GumService.Default;

    public GameTest2() : base("GameTest", 1280, 720, false)
    {

    }
    
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        var gumProject = GumUI.Initialize(this, "GumProject/GumProject.gumx");
        InitTestGum(gumProject);

        ChangeScene(new TitleScene());

        base.Initialize();
    }


    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        GumService.Default.Update(gameTime, _graphicalUiElement);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);

        GumService.Default.Draw();
    }

    public void InitTestGum(GumProjectSave gumProject)
    {
        var screen = gumProject.Screens.Find(item => item.Name == "MainMenu");
        screen.ToGraphicalUiElement(SystemManagers.Default);
        _graphicalUiElement = screen.ToGraphicalUiElement(SystemManagers.Default);
        _graphicalUiElement.AddToManagers(SystemManagers.Default);

        var textTextInstance = _graphicalUiElement.GetGraphicalUiElementByName("TextInstance") as TextRuntime;
        textTextInstance.Text = "From Code!";

        var button = _graphicalUiElement.GetFrameworkElementByName<Button>("ButtonStandardInstance");

        EventHandler clickHandler = null;
        clickHandler = (_, _) =>
        {
            textTextInstance.Text = "Clicked";
            button.Click -= clickHandler;
        };
        button.Click += clickHandler;
    }
}