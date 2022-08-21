namespace SnakesAndLadders.Services
{
    public class DiceService : IDiceService
    {
        public int RollsDice()
        {
            var rand = new Random();
            return rand.Next(1, 7);
        }
    }
}
