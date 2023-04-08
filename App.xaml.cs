using Lightbender_Minecraft_Mod_Manager.Models;

namespace Lightbender_Minecraft_Mod_Manager
{
    public partial class App : Application
    {
        public static Settings CurrentSettings { get; protected set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            CurrentSettings = await Models.Settings.LoadAsync();
        }
    }
}
