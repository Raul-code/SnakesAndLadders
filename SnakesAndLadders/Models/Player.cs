namespace SnakesAndLadders.Models
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int Position { get; set; } // Index 1

        public Player(int playerNumber)
        {
            Position = 1;
            PlayerName = $"Player {playerNumber}";
        }
    }
}
