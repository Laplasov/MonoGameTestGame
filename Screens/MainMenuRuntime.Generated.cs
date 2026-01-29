//Code for MainMenu
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Project1.Components.Castom;
using Project1.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Project1.Screens;
partial class MainMenuRuntime : global::MonoGameGum.GueDeriving.ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("MainMenu", typeof(MainMenuRuntime));
    }
    public ListBoxItemNewCastomRuntime ListBoxItemInstance { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public ButtonStandardRuntime NewGameButton { get; protected set; }
    public ButtonStandardRuntime LoadButton { get; protected set; }
    public ButtonStandardRuntime SettingsButton { get; protected set; }
    public ButtonStandardRuntime ExitButton { get; protected set; }
    public TextRuntime TextInstance3 { get; protected set; }
    public TextBoxCastomNewRuntime TextBoxCastomInstance { get; protected set; }
    public ButtonStandardRuntime OkNewGameButton { get; protected set; }
    public ButtonStandardRuntime ExitNewGameButton { get; protected set; }
    public TextRuntime TextInstance1 { get; protected set; }
    public ButtonStandardRuntime ExitSettings { get; protected set; }
    public TextRuntime TextInstance2 { get; protected set; }
    public ListBoxNewCastomRuntime ListBoxInstance { get; protected set; }
    public ButtonStandardRuntime ExitLoad { get; protected set; }
    public ButtonStandardRuntime DeleteCurrentLoad { get; protected set; }
    public ButtonStandardRuntime DeleteLoad { get; protected set; }
    public ButtonStandardRuntime DeleteAllLoad { get; protected set; }
    public ContainerRuntime ContainerInstance { get; protected set; }
    public ContainerRuntime ContainerInstance3 { get; protected set; }
    public ContainerRuntime ContainerInstance1 { get; protected set; }
    public ContainerRuntime ContainerInstance2 { get; protected set; }
    public WindowCastomRuntime MainWindow { get; protected set; }
    public WindowCastomRuntime NewGame { get; protected set; }
    public WindowCastomRuntime SettingsWindow { get; protected set; }
    public WindowCastomRuntime LoadWindow { get; protected set; }
    public ContainerRuntime Root { get; protected set; }

    public MainMenuRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("MainMenu");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        ListBoxItemInstance = this.GetGraphicalUiElementByName("ListBoxItemInstance") as Project1.Components.Castom.ListBoxItemNewCastomRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        NewGameButton = this.GetGraphicalUiElementByName("NewGameButton") as Project1.Components.Controls.ButtonStandardRuntime;
        LoadButton = this.GetGraphicalUiElementByName("LoadButton") as Project1.Components.Controls.ButtonStandardRuntime;
        SettingsButton = this.GetGraphicalUiElementByName("SettingsButton") as Project1.Components.Controls.ButtonStandardRuntime;
        ExitButton = this.GetGraphicalUiElementByName("ExitButton") as Project1.Components.Controls.ButtonStandardRuntime;
        TextInstance3 = this.GetGraphicalUiElementByName("TextInstance3") as global::MonoGameGum.GueDeriving.TextRuntime;
        TextBoxCastomInstance = this.GetGraphicalUiElementByName("TextBoxCastomInstance") as Project1.Components.Castom.TextBoxCastomNewRuntime;
        OkNewGameButton = this.GetGraphicalUiElementByName("OkNewGameButton") as Project1.Components.Controls.ButtonStandardRuntime;
        ExitNewGameButton = this.GetGraphicalUiElementByName("ExitNewGameButton") as Project1.Components.Controls.ButtonStandardRuntime;
        TextInstance1 = this.GetGraphicalUiElementByName("TextInstance1") as global::MonoGameGum.GueDeriving.TextRuntime;
        ExitSettings = this.GetGraphicalUiElementByName("ExitSettings") as Project1.Components.Controls.ButtonStandardRuntime;
        TextInstance2 = this.GetGraphicalUiElementByName("TextInstance2") as global::MonoGameGum.GueDeriving.TextRuntime;
        ListBoxInstance = this.GetGraphicalUiElementByName("ListBoxInstance") as Project1.Components.Castom.ListBoxNewCastomRuntime;
        ExitLoad = this.GetGraphicalUiElementByName("ExitLoad") as Project1.Components.Controls.ButtonStandardRuntime;
        DeleteCurrentLoad = this.GetGraphicalUiElementByName("DeleteCurrentLoad") as Project1.Components.Controls.ButtonStandardRuntime;
        DeleteLoad = this.GetGraphicalUiElementByName("DeleteLoad") as Project1.Components.Controls.ButtonStandardRuntime;
        DeleteAllLoad = this.GetGraphicalUiElementByName("DeleteAllLoad") as Project1.Components.Controls.ButtonStandardRuntime;
        ContainerInstance = this.GetGraphicalUiElementByName("ContainerInstance") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ContainerInstance3 = this.GetGraphicalUiElementByName("ContainerInstance3") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ContainerInstance1 = this.GetGraphicalUiElementByName("ContainerInstance1") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ContainerInstance2 = this.GetGraphicalUiElementByName("ContainerInstance2") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        MainWindow = this.GetGraphicalUiElementByName("MainWindow") as Project1.Components.Castom.WindowCastomRuntime;
        NewGame = this.GetGraphicalUiElementByName("NewGame") as Project1.Components.Castom.WindowCastomRuntime;
        SettingsWindow = this.GetGraphicalUiElementByName("SettingsWindow") as Project1.Components.Castom.WindowCastomRuntime;
        LoadWindow = this.GetGraphicalUiElementByName("LoadWindow") as Project1.Components.Castom.WindowCastomRuntime;
        Root = this.GetGraphicalUiElementByName("Root") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
