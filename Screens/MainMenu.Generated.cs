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
partial class MainMenu : global::Gum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("MainMenu");
#if DEBUG
if(element == null) throw new System.InvalidOperationException("Could not find an element named MainMenu - did you forget to load a Gum project?");
#endif
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new MainMenu(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(MainMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("MainMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ListBoxItemNewCastom ListBoxItemInstance { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public ButtonStandard NewGameButton { get; protected set; }
    public ButtonStandard LoadButton { get; protected set; }
    public ButtonStandard SettingsButton { get; protected set; }
    public ButtonStandard ExitButton { get; protected set; }
    public TextRuntime TextInstance1 { get; protected set; }
    public ButtonStandard ExitSettings { get; protected set; }
    public TextRuntime TextInstance2 { get; protected set; }
    public ListBoxNewCastom ListBoxInstance { get; protected set; }
    public ButtonStandard ExitLoad { get; protected set; }
    public ContainerRuntime ContainerInstance { get; protected set; }
    public ContainerRuntime ContainerInstance1 { get; protected set; }
    public ContainerRuntime ContainerInstance2 { get; protected set; }
    public WindowCastomSlice MainWindow { get; protected set; }
    public WindowCastomSlice SettingsWindow { get; protected set; }
    public WindowCastomSlice LoadWindow { get; protected set; }
    public ContainerRuntime Root { get; protected set; }

    public MainMenu(InteractiveGue visual) : base(visual)
    {
    }
    public MainMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ListBoxItemInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ListBoxItemNewCastom>(this.Visual,"ListBoxItemInstance");
        TextInstance = this.Visual?.GetGraphicalUiElementByName("TextInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        NewGameButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"NewGameButton");
        LoadButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"LoadButton");
        SettingsButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"SettingsButton");
        ExitButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"ExitButton");
        TextInstance1 = this.Visual?.GetGraphicalUiElementByName("TextInstance1") as global::MonoGameGum.GueDeriving.TextRuntime;
        ExitSettings = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"ExitSettings");
        TextInstance2 = this.Visual?.GetGraphicalUiElementByName("TextInstance2") as global::MonoGameGum.GueDeriving.TextRuntime;
        ListBoxInstance = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ListBoxNewCastom>(this.Visual,"ListBoxInstance");
        ExitLoad = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"ExitLoad");
        ContainerInstance = this.Visual?.GetGraphicalUiElementByName("ContainerInstance") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ContainerInstance1 = this.Visual?.GetGraphicalUiElementByName("ContainerInstance1") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ContainerInstance2 = this.Visual?.GetGraphicalUiElementByName("ContainerInstance2") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        MainWindow = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<WindowCastomSlice>(this.Visual,"MainWindow");
        SettingsWindow = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<WindowCastomSlice>(this.Visual,"SettingsWindow");
        LoadWindow = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<WindowCastomSlice>(this.Visual,"LoadWindow");
        Root = this.Visual?.GetGraphicalUiElementByName("Root") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
