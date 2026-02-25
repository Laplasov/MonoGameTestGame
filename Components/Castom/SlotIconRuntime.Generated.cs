//Code for Castom/SlotIcon (Container)
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

namespace Project1.Components.Castom;
partial class SlotIconRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Castom/SlotIcon", typeof(SlotIconRuntime));
    }
    public ContainerRuntime HpAndSP { get; protected set; }
    public ContainerRuntime BackgroundSlot { get; protected set; }
    public ContainerRuntime HpSpContainer { get; protected set; }
    public ContainerRuntime Slot { get; protected set; }
    public GlassWindowRuntime WindowCastomInstance { get; protected set; }
    public SpriteRuntime Sprite { get; protected set; }
    public SpriteRuntime Hp { get; protected set; }
    public SpriteRuntime Sp { get; protected set; }
    public SpriteRuntime SpBackgroundSlot { get; protected set; }
    public SpriteRuntime HpBackgroundSlot { get; protected set; }

    public float HpValue
    {
        get => Hp.Width;
        set => Hp.Width = value;
    }

    public float SpValue
    {
        get => Sp.Width;
        set => Sp.Width = value;
    }

    public string SpriteSourceFile
    {
        set => Sprite.SourceFileName = value;
    }

    public int SpriteTextureHeight
    {
        get => Sprite.TextureHeight;
        set => Sprite.TextureHeight = value;
    }

    public int SpriteTextureLeft
    {
        get => Sprite.TextureLeft;
        set => Sprite.TextureLeft = value;
    }

    public int SpriteTextureTop
    {
        get => Sprite.TextureTop;
        set => Sprite.TextureTop = value;
    }

    public int SpriteTextureWidth
    {
        get => Sprite.TextureWidth;
        set => Sprite.TextureWidth = value;
    }

    public SlotIconRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Castom/SlotIcon");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        HpAndSP = this.GetGraphicalUiElementByName("HpAndSP") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        BackgroundSlot = this.GetGraphicalUiElementByName("BackgroundSlot") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        HpSpContainer = this.GetGraphicalUiElementByName("HpSpContainer") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        Slot = this.GetGraphicalUiElementByName("Slot") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        WindowCastomInstance = this.GetGraphicalUiElementByName("WindowCastomInstance") as Project1.Components.Castom.GlassWindowRuntime;
        Sprite = this.GetGraphicalUiElementByName("Sprite") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        Hp = this.GetGraphicalUiElementByName("Hp") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        Sp = this.GetGraphicalUiElementByName("Sp") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        SpBackgroundSlot = this.GetGraphicalUiElementByName("SpBackgroundSlot") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        HpBackgroundSlot = this.GetGraphicalUiElementByName("HpBackgroundSlot") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
