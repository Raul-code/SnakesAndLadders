using SnakesAndLadders.Models;
using Xunit;

namespace SnakesAndLadders.Test
{
    public class DiceTest
    {
        private readonly Dice _dice;
        public DiceTest()
        {
            _dice = new Dice();
        }

        [Fact]
        public void RollsDie()
        {
            var result = _dice.RollsDice();

            Assert.True(result >= 0 && result <= 6);
        }
    }
}