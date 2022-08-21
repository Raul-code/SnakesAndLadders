namespace SnakesAndLadders.Services
{
    internal interface IFileService
    {
        Task<T> ReadFileAsync<T>(string filePath);
    }
}