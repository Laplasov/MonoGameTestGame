//Code for SlotIcon (Container)
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Project1.Components.Castom;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Project1.Components;
partial class SlotIconRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("SlotIcon", typeof(SlotIconRuntime));
    }
    public ContainerRuntime Slot { get; protected set; }
    public SpriteRuntime Sprite { get; protected set; }
    public WindowCastomRuntime WindowCastomInstance { get; protected set; }
    public ContainerRuntime BackgroundSlot { get; protected set; }
    public ContainerRuntime HpSpContainer { get; protected set; }
    public SpriteRuntime Hp { get; protected set; }
    public SpriteRuntime Sp { get; protected set; }
    public SpriteRuntime SpBackgroundSlot { get; protected set; }
    public SpriteRuntime HpBackgroundSlot { get; protected set; }

    public SlotIconRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("SlotIcon");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        Slot = this.GetGraphicalUiElementByName("Slot") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        Sprite = this.GetGraphicalUiElementByName("Sprite") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        WindowCastomInstance = this.GetGraphicalUiElementByName("WindowCastomInstance") as Project1.Components.Castom.WindowCastomRuntime;
        BackgroundSlot = this.GetGraphicalUiElementByName("BackgroundSlot") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        HpSpContainer = this.GetGraphicalUiElementByName("HpSpContainer") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        Hp = this.GetGraphicalUiElementByName("Hp") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        Sp = this.GetGraphicalUiElementByName("Sp") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        SpBackgroundSlot = this.GetGraphicalUiElementByName("SpBackgroundSlot") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        HpBackgroundSlot = this.GetGraphicalUiElementByName("HpBackgroundSlot") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
