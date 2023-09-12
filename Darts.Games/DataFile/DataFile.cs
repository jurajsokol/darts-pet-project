using System.Text.Json;

namespace Darts.Games.DataFile
{
    public class DataFile
    {
        private const string FILE_NAME = "data.json";

        public string[] Players { get; set; } = Array.Empty<string>();

        public async Task SaveToFile(string localFolderPath)
        {
            using FileStream createStream = File.Create(Path.Combine(localFolderPath, FILE_NAME));
            await JsonSerializer.SerializeAsync(createStream, this);
            await createStream.DisposeAsync();
        }

        public static DataFile LoadFromFile(string localFolderPath)
        {
            if (File.Exists(Path.Combine(localFolderPath, FILE_NAME))) 
            {
                string jsonString = File.ReadAllText(Path.Combine(localFolderPath, FILE_NAME));
                return JsonSerializer.Deserialize<DataFile>(jsonString)!;
            }

            return new DataFile();
        }
    }
}
