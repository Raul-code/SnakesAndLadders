using SnakesAndLadders.Models;
using SnakesAndLadders.Models.Adornments;

namespace SnakesAndLadders.Services
{
    public interface IDrawService
    {
        void Draw(Player[] players, int turn, Adornment currentAdornment, int diceNumber);
        void DrawBoard(Adornment[] adornments);
        void DrawWinner(Player player);
    }
}
