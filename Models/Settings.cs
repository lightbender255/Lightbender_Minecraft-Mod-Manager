using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;

namespace Lightbender_Minecraft_Mod_Manager.Models
{
    public class Settings
    {
        public string ModPaths { get; set; }
        public ModDirectories ModDirectories { get; set; } = new ModDirectories();

        //private static string ApplicationDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lightbender255 Mod Manager");

#if DEBUG
        private static string SettingsFile = "G:\\Projects\\Minecraft\\Modding\\dev\\Lightbender_Minecraft-Mod-Manager\\testdir\\settings.json";
#else
        private static string SettingsFile = Path.Combine(ApplicationDataPath, "settings.json");
#endif
        private static string SettingsPath => SettingsFile; 

        public Settings() { }

        public static async Task SaveAsync(Settings settings)
        {
            var directoryPath = Path.GetDirectoryName(SettingsPath);
            try
            {
                // BUG: This is not working.  It is not creating the directory in AppData\Roaming\Lightbender255 Mod Manager
                // Cannot create directories with periods in the name.
                // Using a local directory inside the project works fine.

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using var streamWriter = new StreamWriter(SettingsPath);
                var json = JsonConvert.SerializeObject(settings);
                await streamWriter.WriteAsync(json);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static async Task<Settings> LoadAsync()
        {
            //var file = Path.Combine(FileSystem.AppDataDirectory, "settings.json");
#if DEBUG
            var file = "G:\\Projects\\Minecraft\\Modding\\dev\\Lightbender_Minecraft-Mod-Manager\\testdir\\settings.json";
#endif
            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);                
                var settings = JsonConvert.DeserializeObject<Settings>(json);

                // Set the ModDirectories property to the ModPaths property if it is not null.
                if (settings.ModPaths != null)
                {
                    settings.ModDirectories = JsonConvert.DeserializeObject<ModDirectories>(settings.ModPaths);
                }
                await Toast.Make("LBFF-MMM Settings loaded.", duration:CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                return settings;
            }
            else
            {
                return new Settings();
            }
        }
    }

    public class ModDirectories
    {
        [JsonProperty("sourcemodpath")]
        public string SourceModPath { get; set; }

        [JsonProperty("clientmodpath")]
        public string ClientModPath { get; set; }

        [JsonProperty("servermodpath")]
        public string ServerModPath { get; set; }
    }
}
