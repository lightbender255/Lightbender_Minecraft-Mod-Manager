using Newtonsoft.Json;

namespace Lightbender_Minecraft_Mod_Manager.Models
{
    internal class AppSettings
    {
        public ModDirectories ModDirectories { get; set; } = new ModDirectories();

        private static string ApplicationDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ca.lightbender255.lightbender-minecraft-mod-manager");

        private static string SettingsPath => Path.Combine(ApplicationDataPath, "settings.json");

        public AppSettings() { }

        public static async Task SaveAsync(AppSettings settings)
        {
            var filePath = Path.Combine(ApplicationDataPath, "settings.json");

            using (var streamWriter = new StreamWriter(filePath))
            {
                var json = JsonConvert.SerializeObject(settings);
                await streamWriter.WriteAsync(json);
            }
        }

        public static async Task<AppSettings> LoadAsync()
        {
            var file = Path.Combine(FileSystem.AppDataDirectory, "settings.json");
    
            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);
                return JsonConvert.DeserializeObject<AppSettings>(json);
            }
            else
            {
                return new AppSettings();
            }
        }

    }

    public class ModDirectories
    {
        [JsonProperty("sourcemodpath")]
        public string SourceModPath { get; set; }

        [JsonProperty("clientmodpath")]
        public string Issues { get; set; }

        [JsonProperty("servermodpath")]
        public string Sources { get; set; }
    }
}
