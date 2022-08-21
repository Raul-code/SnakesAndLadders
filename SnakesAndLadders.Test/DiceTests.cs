using SnakesAndLadders.Services;
using Xunit;

namespace SnakesAndLadders.Test
{
    public class DiceTests
    {
        private readonly DiceService _dice;
        public DiceTests()
        {
            _dice = new DiceService();
        }

        [Fact]
        public void RollsDie()
        {
            var result = _dice.RollsDice();

            Assert.True(result >= 0 && result <= 6);
        }
    }
}