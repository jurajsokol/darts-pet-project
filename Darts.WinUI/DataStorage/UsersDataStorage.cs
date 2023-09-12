using Darts.WinUI.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace Darts.WinUI.DataStorage
{
    public class UsersDataStorage
    {
        public static async Task SavePlayers(IList<Player> players)
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync("dataFile.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, JsonSerializer.Serialize(players));
        }

        public static async Task<IList<Player>> LoadPLayers()
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync("dataFile.txt", CreationCollisionOption.OpenIfExists);

            String players = await FileIO.ReadTextAsync(sampleFile);

            if (string.IsNullOrEmpty(players))
            {
                return Array.Empty<Player>();
            }
            return JsonSerializer.Deserialize<Player[]>(players);
        }
    }
}
