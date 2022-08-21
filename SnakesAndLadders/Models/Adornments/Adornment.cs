namespace SnakesAndLadders.Models.Adornments
{
    public abstract class Adornment
    {
        public int InitialPosition { get; set; }
        public int FinalPosition { get; set; }

        protected Adornment(int initialPosition, int finalPosition)
        {
            InitialPosition = initialPosition;
            FinalPosition = finalPosition;
        }
    }
}