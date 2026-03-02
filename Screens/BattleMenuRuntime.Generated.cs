//Code for BattleMenu
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

namespace Project1.Screens;
partial class BattleMenuRuntime : global::MonoGameGum.GueDeriving.ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("BattleMenu", typeof(BattleMenuRuntime));
    }
    public TextRuntime EnteringText { get; protected set; }
    public SlotIconRuntime SlotEnemy4 { get; protected set; }
    public SlotIconRuntime SlotEnemy5 { get; protected set; }
    public SlotIconRuntime SlotEnemy6 { get; protected set; }
    public SlotIconRuntime SlotEnemy1 { get; protected set; }
    public SlotIconRuntime SlotEnemy2 { get; protected set; }
    public SlotIconRuntime SlotEnemy3 { get; protected set; }
    public SlotIconRuntime SlotAllay1 { get; protected set; }
    public SlotIconRuntime SlotAllay2 { get; protected set; }
    public SlotIconRuntime SlotAllay3 { get; protected set; }
    public SlotIconRuntime SlotAllay4 { get; protected set; }
    public SlotIconRuntime SlotAllay5 { get; protected set; }
    public SlotIconRuntime SlotAllay6 { get; protected set; }
    public SpriteRuntime BackgroundDescription1 { get; protected set; }
    public TextRuntime UnitBarText { get; protected set; }
    public GlassWindowRuntime WindowCastomInstance12 { get; protected set; }
    public SpriteRuntime BackgroundDescription { get; protected set; }
    public TextRuntime TextDescription { get; protected set; }
    public GlassWindowRuntime FrameDescription { get; protected set; }
    public ButtonGameCastomRuntime SkillButton { get; protected set; }
    public ButtonGameCastomRuntime ItemButton { get; protected set; }
    public ButtonGameCastomRuntime WaitButton { get; protected set; }
    public ButtonGameCastomRuntime RunButton { get; protected set; }
    public ButtonGameCastomRuntime OptionButton1 { get; protected set; }
    public ButtonGameCastomRuntime OptionButton2 { get; protected set; }
    public ButtonGameCastomRuntime OptionButton3 { get; protected set; }
    public ButtonGameCastomRuntime OptionButton4 { get; protected set; }
    public SpriteRuntime ActionsTitle { get; protected set; }
    public ContainerRuntime LeftActionButtons { get; protected set; }
    public ContainerRuntime RightActionButtons { get; protected set; }
    public ContainerRuntime DescriptionWindow { get; protected set; }
    public TextRuntime ActionsTitleText { get; protected set; }
    public ContainerRuntime EnemySlots { get; protected set; }
    public ContainerRuntime AllaySlots { get; protected set; }
    public ContainerRuntime UnitBar { get; protected set; }
    public ContainerRuntime ActionsBar { get; protected set; }
    public GlassWindowRuntime EnteringTextWindow { get; protected set; }
    public ContainerRuntime Root { get; protected set; }

    public BattleMenuRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("BattleMenu");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        EnteringText = this.GetGraphicalUiElementByName("EnteringText") as global::MonoGameGum.GueDeriving.TextRuntime;
        SlotEnemy4 = this.GetGraphicalUiElementByName("SlotEnemy4") as Project1.Components.Castom.SlotIconRuntime;
        SlotEnemy5 = this.GetGraphicalUiElementByName("SlotEnemy5") as Project1.Components.Castom.SlotIconRuntime;
        SlotEnemy6 = this.GetGraphicalUiElementByName("SlotEnemy6") as Project1.Components.Castom.SlotIconRuntime;
        SlotEnemy1 = this.GetGraphicalUiElementByName("SlotEnemy1") as Project1.Components.Castom.SlotIconRuntime;
        SlotEnemy2 = this.GetGraphicalUiElementByName("SlotEnemy2") as Project1.Components.Castom.SlotIconRuntime;
        SlotEnemy3 = this.GetGraphicalUiElementByName("SlotEnemy3") as Project1.Components.Castom.SlotIconRuntime;
        SlotAllay1 = this.GetGraphicalUiElementByName("SlotAllay1") as Project1.Components.Castom.SlotIconRuntime;
        SlotAllay2 = this.GetGraphicalUiElementByName("SlotAllay2") as Project1.Components.Castom.SlotIconRuntime;
        SlotAllay3 = this.GetGraphicalUiElementByName("SlotAllay3") as Project1.Components.Castom.SlotIconRuntime;
        SlotAllay4 = this.GetGraphicalUiElementByName("SlotAllay4") as Project1.Components.Castom.SlotIconRuntime;
        SlotAllay5 = this.GetGraphicalUiElementByName("SlotAllay5") as Project1.Components.Castom.SlotIconRuntime;
        SlotAllay6 = this.GetGraphicalUiElementByName("SlotAllay6") as Project1.Components.Castom.SlotIconRuntime;
        BackgroundDescription1 = this.GetGraphicalUiElementByName("BackgroundDescription1") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        UnitBarText = this.GetGraphicalUiElementByName("UnitBarText") as global::MonoGameGum.GueDeriving.TextRuntime;
        WindowCastomInstance12 = this.GetGraphicalUiElementByName("WindowCastomInstance12") as Project1.Components.Castom.GlassWindowRuntime;
        BackgroundDescription = this.GetGraphicalUiElementByName("BackgroundDescription") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        TextDescription = this.GetGraphicalUiElementByName("TextDescription") as global::MonoGameGum.GueDeriving.TextRuntime;
        FrameDescription = this.GetGraphicalUiElementByName("FrameDescription") as Project1.Components.Castom.GlassWindowRuntime;
        SkillButton = this.GetGraphicalUiElementByName("SkillButton") as Project1.Components.Castom.ButtonGameCastomRuntime;
        ItemButton = this.GetGraphicalUiElementByName("ItemButton") as Project1.Components.Castom.ButtonGameCastomRuntime;
        WaitButton = this.GetGraphicalUiElementByName("WaitButton") as Project1.Components.Castom.ButtonGameCastomRuntime;
        RunButton = this.GetGraphicalUiElementByName("RunButton") as Project1.Components.Castom.ButtonGameCastomRuntime;
        OptionButton1 = this.GetGraphicalUiElementByName("OptionButton1") as Project1.Components.Castom.ButtonGameCastomRuntime;
        OptionButton2 = this.GetGraphicalUiElementByName("OptionButton2") as Project1.Components.Castom.ButtonGameCastomRuntime;
        OptionButton3 = this.GetGraphicalUiElementByName("OptionButton3") as Project1.Components.Castom.ButtonGameCastomRuntime;
        OptionButton4 = this.GetGraphicalUiElementByName("OptionButton4") as Project1.Components.Castom.ButtonGameCastomRuntime;
        ActionsTitle = this.GetGraphicalUiElementByName("ActionsTitle") as global::MonoGameGum.GueDeriving.SpriteRuntime;
        LeftActionButtons = this.GetGraphicalUiElementByName("LeftActionButtons") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        RightActionButtons = this.GetGraphicalUiElementByName("RightActionButtons") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        DescriptionWindow = this.GetGraphicalUiElementByName("DescriptionWindow") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ActionsTitleText = this.GetGraphicalUiElementByName("ActionsTitleText") as global::MonoGameGum.GueDeriving.TextRuntime;
        EnemySlots = this.GetGraphicalUiElementByName("EnemySlots") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        AllaySlots = this.GetGraphicalUiElementByName("AllaySlots") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        UnitBar = this.GetGraphicalUiElementByName("UnitBar") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ActionsBar = this.GetGraphicalUiElementByName("ActionsBar") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        EnteringTextWindow = this.GetGraphicalUiElementByName("EnteringTextWindow") as Project1.Components.Castom.GlassWindowRuntime;
        Root = this.GetGraphicalUiElementByName("Root") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
