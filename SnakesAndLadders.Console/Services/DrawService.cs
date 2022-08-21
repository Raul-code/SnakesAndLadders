using SnakesAndLadders.Models;
using SnakesAndLadders.Models.Adornments;
using SnakesAndLadders.Services;

namespace SnakesAndLadders.ConsoleApp.Services
{
    internal class DrawService : IDrawService
    {
        public void Draw(Player[] players, int turn, Adornment currentAdornment, int diceNumber)
        {
            Console.WriteLine($"\nDice number: {diceNumber}");

            if (currentAdornment != null)
            {
                if (currentAdornment is Snake)
                {
                    Console.WriteLine($"The square {currentAdornment.InitialPosition} contains a snake");
                }
                else if(currentAdornment is Ladder)
                {
                    Console.WriteLine($"The square {currentAdornment.InitialPosition} contains a ladder");
                }
            }

            foreach (var player in players)
            {
                Console.WriteLine($"Position {player.PlayerName} : {player.Position}");
            }

            Console.WriteLine($"Next turn: {turn}");
        }

        public void DrawWinner(Player player)
        {
            Console.WriteLine($"The winner is {player.PlayerName}!!");
        }

        public void DrawBoard(Adornment[] adornments)
        {
            foreach (var adornment in adornments)
            {
                if (adornment != null)
                {
                    if (adornment is Snake)
                    {
                        Console.WriteLine($"Snake from {adornment.InitialPosition} to {adornment.FinalPosition}");
                    }
                    else if (adornment is Ladder)
                    {
                        Console.WriteLine($"Ladder from {adornment.InitialPosition} to {adornment.FinalPosition}");
                    }
                }
            }
        }
    }
}
