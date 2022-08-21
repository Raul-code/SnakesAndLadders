using SnakesAndLadders;
using SnakesAndLadders.ConsoleApp.Services;
using SnakesAndLadders.Services;

Console.WriteLine("Welcome to SnakesAndLadders");

int playersNumber = 0;
while (playersNumber < 2)
{
    Console.WriteLine("Enter the number of players");
    var playersNumberString = Console.ReadLine();

    if (playersNumberString != null && !int.TryParse(playersNumberString, out playersNumber))
    {
        Console.WriteLine("The number of players must be greater than 2");
    }
}

Game game = new(new NextTurnService(), new DrawService(), new DiceService());

await game.Initialize(playersNumber);

Console.WriteLine("Press ESC to exit");
Console.WriteLine("Press SPACE to roll a dice");

await game.StartGame();