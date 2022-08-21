using System.Threading.Tasks;
using Xunit;

namespace SnakesAndLadders.Test
{
    public class GameTest
    {
        [Fact]
        public async Task CheckInitialPositions()
        {
            Game game = new Game(null, null);
            await game.Initialize(2);

            foreach (var player in game.Players)
            {
                Assert.True(player.PositionToShow == 1);
            }
        }

        [Fact]
        public async void CheckMovement()
        {
            var game = new Game(null, null);
            await game.Initialize(2);

            game.CalculateNewPosition(3);
            Assert.True(game.Players[0].PositionToShow == 4);
        }
    }
}
