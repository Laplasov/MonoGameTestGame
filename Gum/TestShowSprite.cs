using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Game_Library;
using MonoGameGum;
using System;


namespace Project1.Gum
{
    public class TestShowSprite
    {
        private Panel _titleScreenButtonsPanel;
        private Image etraImage;
        public void CreateTitlePanel(Texture2D textureEtra)
        {

            _titleScreenButtonsPanel = new Panel();
            _titleScreenButtonsPanel.AddToRoot();

            etraImage = new Image();
            etraImage.Texture = textureEtra;
            etraImage.X = 10;
            etraImage.Y = 10;

            _titleScreenButtonsPanel.AddChild(etraImage);
        }

        public void Update(GameTime gameTime)
        {
            if (etraImage != null)
            {
                // Example: Move right at 50 pixels per second
                etraImage.X += 50f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Or oscillate back and forth using sine wave
                // etraImage.X = 10 + (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2) * 100;

                // Or bounce animation
                // etraImage.Y = 10 + (float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3)) * 50;
            }
        }

    }
}
