using Newtonsoft.Json;

namespace Lightbender_Minecraft_Mod_Manager.Models
{
    internal class Settings
    {
        public ModDirectories ModDirectories { get; set; } = new ModDirectories();

        private static string ApplicationDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lightbender255 Mod Manager");

        private static string SettingsPath => Path.Combine(ApplicationDataPath, "settings.json");

        public Settings() { }

        public static async Task SaveAsync(Settings settings)
        {
            var filePath = Path.Combine(ApplicationDataPath, "settings.json");
            var directoryPath = Path.GetDirectoryName(filePath);
            try
            {
                // BUG: This is not working.  It is not creating the directory in AppData\Roaming\Lightbender255 Mod Manager
                // Cannot create directories with periods in the name.
                // Using a local directory inside the project works fine.

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
#if DEBUG
                filePath = "G:\\Projects\\Minecraft\\Modding\\dev\\Lightbender_Minecraft-Mod-Manager\\testdir\\settings.json";
#endif
                using var streamWriter = new StreamWriter(filePath);
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
            var file = "G:\\Projects\\Minecraft\\Modding\\dev\\Lightbender_Minecraft-Mod-Manager\\testdir\\settings.json";

            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);
                return JsonConvert.DeserializeObject<Settings>(json);
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
