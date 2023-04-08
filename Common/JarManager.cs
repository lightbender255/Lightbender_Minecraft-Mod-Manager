using System.IO.Compression;


namespace Lightbender_Minecraft_Mod_Manager.Common
{
    public static class JarManager
    {
        public static void Decompress(string jarFilePath, string outputDirectory)
        {
            using (ZipArchive archive = ZipFile.OpenRead(jarFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string outputPath = Path.Combine(outputDirectory, entry.FullName);

                    if (entry.Name == "")
                    {
                        // If the entry is a directory, create it in the output directory
                        Directory.CreateDirectory(outputPath);
                    }
                    else
                    {
                        // If the entry is a file, extract it to the output directory
                        entry.ExtractToFile(outputPath, true);
                    }
                }
            }
        }

        public static byte[] ExtractFile(string jarFilePath, string filePathInJar)
        {
            using (ZipArchive archive = ZipFile.OpenRead(jarFilePath))
            {
                ZipArchiveEntry entry = archive.GetEntry(filePathInJar);

                if (entry == null)
                {
                    throw new FileNotFoundException($"The file {filePathInJar} was not found in the JAR file.");
                }

                using (MemoryStream memoryStream = new MemoryStream())
                using (Stream stream = entry.Open())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
    }

}
}
