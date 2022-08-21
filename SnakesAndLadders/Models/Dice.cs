namespace SnakesAndLadders.Models
{
    public class Dice
    {
        public int RollsDice()
        {
            var rand = new Random();
            return rand.Next(1, 7);
        }
    }
}
