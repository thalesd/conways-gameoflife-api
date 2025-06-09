using Conways_GameOfLife_API.Configuration;
using Conways_GameOfLife_API.Data;
using Microsoft.Extensions.Options;
using System.Text;

namespace Conways_GameOfLife_API.Services
{
    public class BoardService
    {
        private readonly InMemoryBoardStore _inMemoryStore;
        private readonly GameOfLifeService _gameService; 
        private readonly APIConfig _config;

        public BoardService(InMemoryBoardStore inMemoryStore, GameOfLifeService gameService, IOptions<APIConfig> config)
        {
            _inMemoryStore = inMemoryStore;
            _gameService = gameService;
            _config = config.Value;
        }

        public Guid AddBoard(bool[,] board)
        {
            return _inMemoryStore.AddBoard(board);
        }

        public bool[,]? GetNext(Guid id)
        {
            var board = _inMemoryStore.GetBoard(id);
            if (board == null) return null;

            var next = _gameService.GetNextState(board);
            _inMemoryStore.UpdateBoard(id, next);
            return next;
        }

        public bool[,]? Advance(Guid id, int steps)
        {
            var board = _inMemoryStore.GetBoard(id);
            if (board == null) return null;

            for (int i = 0; i < steps; i++)
                board = _gameService.GetNextState(board);

            _inMemoryStore.UpdateBoard(id, board);
            return board;
        }

        public (bool[,]? state, bool success, string reason) GetFinal(Guid id)
        {
            var board = _inMemoryStore.GetBoard(id);
            if (board == null) return (null, false, "Board was not found");

            var seen = new HashSet<string>();
            for (int i = 0; i < _config.MaxIterations; i++)
            {
                var boardString = ConvertBoardToString(board);
                if (!seen.Add(boardString))
                    return (board, true, "Reached simulation oscillation state");

                var next = _gameService.GetNextState(board);
                if (_gameService.BoardsEqual(board, next))
                    return (next, true, "Reached simulation static state"); 

                board = next;
            }

            return (null, false, "Exceeded maximum configured iterations");
        }

        private string ConvertBoardToString(bool[,] board)
        {
            var sb = new StringBuilder();
            for (int r = 0; r < board.GetLength(0); r++)
                for (int c = 0; c < board.GetLength(1); c++)
                    sb.Append(board[r, c] ? '1' : '0');
            return sb.ToString();
        }
    }
}
