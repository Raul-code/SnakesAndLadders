namespace SnakesAndLadders.Models
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int Position { get; set; }
        public int PositionToShow => Position + 1; 

        public Player(int playerNumber)
        {
            Position = 0;
            PlayerName = $"Player {playerNumber}";
        }
    }
}
