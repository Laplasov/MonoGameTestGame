using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library;
using MonoGameGum;
using System;

namespace Project1.Gum;

public static class GumBuilder
{
    public static void InitializeGum(Core core)
    {
        GumService.Default.Initialize(core, DefaultVisualsVersion.V3);
        GumService.Default.ContentLoader.XnaContentManager = Core.Content;

        FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);
        FrameworkElement.GamePadsForUiControl.AddRange(GumService.Default.Gamepads);

        FrameworkElement.TabReverseKeyCombos.Add(
           new KeyCombo() { PushedKey = Microsoft.Xna.Framework.Input.Keys.Up });
        FrameworkElement.TabKeyCombos.Add(
           new KeyCombo() { PushedKey = Microsoft.Xna.Framework.Input.Keys.Down });
    }

}