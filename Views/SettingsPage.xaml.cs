using Lightbender_Minecraft_Mod_Manager.ViewModels;

namespace Lightbender_Minecraft_Mod_Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel _viewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }
    }
}
