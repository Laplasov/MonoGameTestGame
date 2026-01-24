using Gum.Forms;
using Gum.Wireframe;
using MonoGameGum.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Button = Gum.Forms.Controls.Button;

namespace Project1.UI
{
    public static class GraphicalUiElementExtensions
    {
        public static Button BindButton(this GraphicalUiElement element, string buttonName, EventHandler handler)
        {
            var button = element.GetFrameworkElementByName<Button>(buttonName);
            if (button != null)
            {
                button.Click += handler;
                GameCore.RegisterEventCleanup(() => button.Click -= handler);
            }
            return button;
        }
    }
}
