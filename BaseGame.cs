using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Game_Library;
using MonoGame_Game_Library.Graphics;
using MonoGame_Game_Library.Input;
using MonoGameGum;
using Gum.Forms;
using Gum.Forms.Controls;

namespace Project1;

public class BaseGame : Core
{
    GumService GumUI => GumService.Default;
    public BaseGame() : base("GameTest", 1280, 720, true)
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
        GumUI.Initialize(this, DefaultVisualsVersion.V3);
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
        GumUI.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        GumUI.Draw();
        base.Draw(gameTime);
    }
}