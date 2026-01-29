//Code for Castom/ButtonCastom (Controls/ButtonStandard)
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Project1.Components.Controls;
using RenderingLibrary.Graphics;
using System.Linq;
using System.Linq;

namespace Project1.Components.Castom;
partial class ButtonCastomRuntime : ButtonStandardRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Castom/ButtonCastom", typeof(ButtonCastomRuntime));
    }

    public ButtonCastomRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Castom/ButtonCastom");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        base.AfterFullCreation();
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
