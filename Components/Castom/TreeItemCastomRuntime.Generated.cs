//Code for Castom/TreeItemCastom (Container)
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Project1.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Project1.Components.Castom;
partial class TreeItemCastomRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Castom/TreeItemCastom", typeof(TreeItemCastomRuntime));
    }
    public TreeViewToggleRuntime ToggleButtonInstance { get; protected set; }
    public ListBoxItemRuntime ListBoxItemInstance { get; protected set; }
    public ContainerRuntime InnerPanelInstance { get; protected set; }

    public TreeItemCastomRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Castom/TreeItemCastom");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        ToggleButtonInstance = this.GetGraphicalUiElementByName("ToggleButtonInstance") as Project1.Components.Controls.TreeViewToggleRuntime;
        ListBoxItemInstance = this.GetGraphicalUiElementByName("ListBoxItemInstance") as Project1.Components.Controls.ListBoxItemRuntime;
        InnerPanelInstance = this.GetGraphicalUiElementByName("InnerPanelInstance") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
