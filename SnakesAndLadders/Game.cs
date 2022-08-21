using SnakesAndLadders.Models;
using SnakesAndLadders.Models.Adornments;
using SnakesAndLadders.Services;

namespace SnakesAndLadders
{
    public class Game
    {
        private readonly IFileService _fileService;
        private readonly INextTurnService _nextTurnService;
        private readonly IDrawService _drawService;

        private Board board;
        private Dice dice;
        private int playerTurn;

        private const string resourcesPath = "Resources";
        private const string fileName = "SnakesAndLaddersConfig.json";

        public bool GameFinished { get; set; }
        public Player[] Players { get; set; }
        public int Turn => playerTurn + 1;
        public Adornment CurrentAdornment { get; set; }
        public int DiceNumber { get; set; }


        public Game(INextTurnService nextTurnService, IDrawService drawService)
        {
            _fileService = new FileService();
            _nextTurnService = nextTurnService;
            _drawService = drawService;

            dice = new Dice();

            playerTurn = 0;
            GameFinished = false;
        }

        public async Task Initialize(int playersNumber)
        {
            if (playersNumber < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(playersNumber));
            }

            var filePath = Path.Combine(resourcesPath, fileName);
            var boardSettings = await _fileService.ReadFileAsync<BoardSettings>(filePath);

            List<Adornment> adornments = new();
            foreach (var item in boardSettings.SquareAdorned)
            {
                CreateAdornments(adornments, item);
            }

            board = new Board(boardSettings.Rows, boardSettings.Columns, adornments);

            Players = new Player[playersNumber];
            for (int i = 0; i < playersNumber; i++)
            {
                Players[i] = new Player(i + 1);
            }
        }

        public async Task StartGame()
        {
            _drawService.DrawBoard(board.Adornments);

            while (!GameFinished)
            {
                await NextTurnAsync();
                _drawService.Draw(Players, Turn, CurrentAdornment, DiceNumber);
            }

            _drawService.DrawWinner(Players[playerTurn]);
        }

        private void CreateAdornments(List<Adornment> adornments, SquaredAdorned item)
        {
            if (item.InitialPosition < item.FinalPosition)
            {
                adornments.Add(new Ladder(item.InitialPosition, item.FinalPosition));
            }
            else if (item.InitialPosition > item.FinalPosition)
            {
                adornments.Add(new Snake(item.InitialPosition, item.FinalPosition));
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        private async Task NextTurnAsync()
        {
            CurrentAdornment = null;

            await _nextTurnService.NextTurnAsync();
            DiceNumber = dice.RollsDice();
            CalculateNewPosition(DiceNumber);

            if (Players[playerTurn].Position == board.BoardSize - 1)
            {
                GameFinished = true;
                return;
            }
            else
            {
                CheckForAdornment();
            }

            NextTurn();
        }

        public void CalculateNewPosition(int diceNumber)
        {
            var newPosition = diceNumber + Players[playerTurn].Position;
            if (newPosition <= board.BoardSize - 1)
            {
                Players[playerTurn].Position = newPosition;
            }
        }

        private void CheckForAdornment()
        {
            CurrentAdornment = board.Adornments[Players[playerTurn].Position];
            if (CurrentAdornment != null)
            {
                Players[playerTurn].Position = CurrentAdornment.FinalPosition - 1;
            }
        }

        private void NextTurn() => playerTurn = (playerTurn + 1) % Players.Length;
    }
}
