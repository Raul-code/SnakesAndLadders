using Newtonsoft.Json;

namespace SnakesAndLadders.Services
{
    internal class FileService : IFileService
    {
        public async Task<T> ReadFileAsync<T>(string filePath)
        {
            string json;
            using (StreamReader r = new(filePath))
            {
                json = await r.ReadToEndAsync();
            }

            T items = JsonConvert.DeserializeObject<T>(json);
            return items;
        }
    }
}
