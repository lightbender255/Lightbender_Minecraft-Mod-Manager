using Lightbender_Minecraft_Mod_Manager.ViewModels;

namespace Lightbender_Minecraft_Mod_Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            BindingContext = new SettingsViewModel();
            InitializeComponent();
        }
    }
}
