using SnakesAndLadders.Services;

namespace SnakesAndLadders.ConsoleApp.Services
{
    internal class NextTurnService : INextTurnService
    {
        public Task NextTurnAsync()
        {
            var key = Console.ReadKey();
            while (key.KeyChar != ' ')
            {
                key = Console.ReadKey();
            }
            return Task.CompletedTask;
        }
    }
}
