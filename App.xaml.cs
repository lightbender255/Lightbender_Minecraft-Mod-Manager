using CommunityToolkit.Maui.Alerts;
using Lightbender_Minecraft_Mod_Manager.Models;

namespace Lightbender_Minecraft_Mod_Manager
{
    public partial class App : Application
    {
        public static Settings LoadedSettings { get; protected set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            LoadedSettings = await Models.Settings.LoadAsync();

            await Toast.Make($"LBFF-MMM Settings loaded.\n Client: {LoadedSettings.ModDirectories.ClientModPath}", duration: CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
    }
}
