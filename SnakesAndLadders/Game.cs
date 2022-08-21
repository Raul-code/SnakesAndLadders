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
        private readonly IDiceService _diceService;

        private Board board;
        private int playerTurn;

        private const string assetsPath = "Assets";
        private string fileName;

        public bool IsFinished { get; set; }
        public Player[] Players { get; set; }
        public int Turn => playerTurn + 1;
        public Adornment CurrentAdornment { get; set; }
        public int DiceNumber { get; set; }


        public Game(INextTurnService nextTurnService, IDrawService drawService, IDiceService diceService, string fileName = "SnakesAndLaddersConfig.json")
        {
            _fileService = new FileService();
            _nextTurnService = nextTurnService;
            _drawService = drawService;
            _diceService = diceService;

            playerTurn = 0;
            IsFinished = false;

            this.fileName = fileName;
        }

        public async Task Initialize(int playersNumber)
        {
            if (playersNumber < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(playersNumber));
            }

            var filePath = Path.Combine(assetsPath, fileName);
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

            while (!IsFinished)
            {
                await NextTurnAsync();
                _drawService.Draw(Players, Turn, CurrentAdornment, DiceNumber);
            }

            if (IsWinSquare())
            {
                _drawService.DrawWinner(Players[playerTurn]);
            }
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

            if (!await _nextTurnService.NextTurnAsync())
            {
                IsFinished = true;
                return;
            }

            DiceNumber = _diceService.RollsDice();

            var newPosition = DiceNumber + Players[playerTurn].Position;
            if (newPosition <= board.BoardSize)
            {
                Players[playerTurn].Position = newPosition;
            }

            if (IsWinSquare())
            {
                IsFinished = true;
                return;
            }
            else
            {
                CheckForAdornment();
            }

            NextTurn();
        }

        private bool IsWinSquare()
        {
            return Players[playerTurn].Position == board.BoardSize;
        }

        private void CheckForAdornment()
        {
            CurrentAdornment = board.Adornments[Players[playerTurn].Position - 1];
            if (CurrentAdornment != null)
            {
                Players[playerTurn].Position = CurrentAdornment.FinalPosition;
            }
        }

        private void NextTurn() => playerTurn = (playerTurn + 1) % Players.Length;
    }
}
