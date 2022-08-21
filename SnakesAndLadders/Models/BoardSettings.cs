namespace SnakesAndLadders.Models;

public class BoardSettings
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public SquaredAdorned[] SquareAdorned { get; set; }
}

public class SquaredAdorned
{
    public int InitialPosition { get; set; }
    public int FinalPosition { get; set; }
}