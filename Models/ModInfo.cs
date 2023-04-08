using Lightbender_Minecraft_Mod_Manager.Common;
using Newtonsoft.Json;

namespace Lightbender_Minecraft_Mod_Manager.Models
{
    public class ModInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("contact")]
        public ModContact Contact { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("authors")]
        public List<ModAuthor> Authors { get; set; }

        [JsonProperty("depends")]
        public Dictionary<string, string> Depends { get; set; }

        [JsonProperty("breaks")]
        public Dictionary<string, string> Breaks { get; set; }

        [JsonProperty("suggests")]
        public Dictionary<string, string> Suggests { get; set; }

        [JsonProperty("entrypoints")]
        public Dictionary<string, List<string>> EntryPoints { get; set; }

        public static IEnumerable<ModInfo> LoadAll(string path)
        {
            /* example code that should probably be used
             * string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extension to load the *.notes.txt files.
            return Directory
                // Select the filenames from the directory
                .EnumerateFiles(appDataPath,"*.notes.txt")
                // Each file name is used to load a note
                .Select(filename => Note.Load(Path.GetFileName(filename)))
                // with the final collection of notes, order them by date
                .OrderByDescending(note => note.Date);
             */
            var mods = new List<ModInfo>();
            var modFiles = Directory.GetFiles(path, "*.jar");
            // TODO: Hardcoded file name is probably bad. KLV 20230407
            var fileName = "fabric.mod.json";
            foreach (var modFile in modFiles)
            {
                var modInfoFile = JarManager.ExtractFile(modFile, fileName).ToString();
                var mod = JsonConvert.DeserializeObject<ModInfo>(modInfoFile);
                mods.Add(mod);
            }
            return mods;
        }
    }

    public class ModContact
    {
        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("issues")]
        public string Issues { get; set; }

        [JsonProperty("sources")]
        public string Sources { get; set; }
    }

    public class ModAuthor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact")]
        public ModAuthorContact Contact { get; set; }
    }

    public class ModAuthorContact
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }
    }


}
