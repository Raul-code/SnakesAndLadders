using Moq;
using SnakesAndLadders.Services;
using System.Threading.Tasks;
using Xunit;

namespace SnakesAndLadders.Test
{
    public class GameTests
    {
        private const string boardFileName = "SnakesAndLaddersConfigMock.json";

        [Fact]
        public async Task CheckInitialPositions()
        {
            Game game = new Game(null, null, null, boardFileName);
            await game.Initialize(2);

            foreach (var player in game.Players)
            {
                Assert.True(player.Position == 1);
            }
        }

        [Fact]
        public async Task CheckMovement()
        {
            var diceServiceMock = new Mock<IDiceService>();
            var nextTurnServiceMock = new Mock<INextTurnService>();
            var drawServiceMock = new Mock<IDrawService>();

            TaskCompletionSource taskCompletionSource = new();
            diceServiceMock.Setup(x => x.RollsDice()).Returns(3);
            nextTurnServiceMock.SetupSequence(x => x.NextTurnAsync()).ReturnsAsync(true).ReturnsAsync(false);

            var game = new Game(nextTurnServiceMock.Object, drawServiceMock.Object, diceServiceMock.Object, boardFileName);
            await game.Initialize(2);
            await game.StartGame();

            Assert.Equal(4, game.Players[0].Position);
        }

        [Fact]
        public async Task CheckMultipleMovement()
        {
            var diceServiceMock = new Mock<IDiceService>();
            var nextTurnServiceMock = new Mock<INextTurnService>();
            var drawServiceMock = new Mock<IDrawService>();

            TaskCompletionSource taskCompletionSource = new();
            diceServiceMock.SetupSequence(x => x.RollsDice()).Returns(3).Returns(4).Returns(4);
            nextTurnServiceMock.SetupSequence(x => x.NextTurnAsync()).ReturnsAsync(true).ReturnsAsync(true).ReturnsAsync(true).ReturnsAsync(false);

            var game = new Game(nextTurnServiceMock.Object, drawServiceMock.Object, diceServiceMock.Object, boardFileName);
            await game.Initialize(2);
            await game.StartGame();

            Assert.Equal(8, game.Players[0].Position);
        }

        [Fact]
        public async Task CheckWinGame()
        {
            var diceServiceMock = new Mock<IDiceService>();
            var nextTurnServiceMock = new Mock<INextTurnService>();
            var drawServiceMock = new Mock<IDrawService>();

            TaskCompletionSource taskCompletionSource = new();
            diceServiceMock.SetupSequence(x => x.RollsDice()).Returns(3);
            nextTurnServiceMock.SetupSequence(x => x.NextTurnAsync()).ReturnsAsync(true).ReturnsAsync(false);

            var game = new Game(nextTurnServiceMock.Object, drawServiceMock.Object, diceServiceMock.Object, boardFileName);
            await game.Initialize(2);

            game.Players[0].Position = 97;

            await game.StartGame();

            Assert.Equal(100, game.Players[0].Position);
            Assert.True(game.IsFinished);
        }

        [Fact]
        public async Task CheckPositionBiggerThanLastSquareGame()
        {
            var diceServiceMock = new Mock<IDiceService>();
            var nextTurnServiceMock = new Mock<INextTurnService>();
            var drawServiceMock = new Mock<IDrawService>();

            TaskCompletionSource taskCompletionSource = new();
            diceServiceMock.SetupSequence(x => x.RollsDice()).Returns(4);
            nextTurnServiceMock.SetupSequence(x => x.NextTurnAsync()).ReturnsAsync(true).ReturnsAsync(false);

            var game = new Game(nextTurnServiceMock.Object, drawServiceMock.Object, diceServiceMock.Object, boardFileName);
            await game.Initialize(2);

            game.Players[0].Position = 97;

            await game.StartGame();

            Assert.Equal(97, game.Players[0].Position);
        }

        [Fact]
        public async Task CheckPlayerMovement()
        {
            var diceServiceMock = new Mock<IDiceService>();
            var nextTurnServiceMock = new Mock<INextTurnService>();
            var drawServiceMock = new Mock<IDrawService>();

            TaskCompletionSource taskCompletionSource = new();
            diceServiceMock.SetupSequence(x => x.RollsDice()).Returns(4);
            nextTurnServiceMock.SetupSequence(x => x.NextTurnAsync()).ReturnsAsync(true).ReturnsAsync(false);

            var game = new Game(nextTurnServiceMock.Object, drawServiceMock.Object, diceServiceMock.Object, boardFileName);
            await game.Initialize(2);

            var initialPosition = game.Players[0].Position;

            game.Players[0].Position = 1;

            await game.StartGame();

            Assert.Equal(initialPosition + 4, game.Players[0].Position);
        }
    }
}
