using SnakesAndLadders.Models.Adornments;

namespace SnakesAndLadders.Models
{
    internal class Board
    {
        public Adornment[] Adornments { get; set; }

        public int Rows { get; set; }
        public int Columns { get; set; }

        public int BoardSize => Rows * Columns;

        public Board(int rows, int columns, List<Adornment> adornmentList)
        {
            Rows = rows;
            Columns = columns;

            if (BoardSize <= 0) throw new ArgumentOutOfRangeException("BoardSize");

            Adornments = new Adornment[BoardSize];

            foreach (var adornment in adornmentList)
            {
                Adornments[adornment.InitialPosition - 1] = adornment;
            }
        }
    }
}
