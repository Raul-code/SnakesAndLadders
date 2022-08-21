using SnakesAndLadders.Services;

namespace SnakesAndLadders.ConsoleApp.Services
{
    internal class NextTurnService : INextTurnService
    {
        public Task<bool> NextTurnAsync()
        {
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Spacebar)
                {
                    return Task.FromResult(true);
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return Task.FromResult(false);
                }
            }
        }
    }
}
