using Conways_GameOfLife_API.Configuration;
using Conways_GameOfLife_API.Data;
using Microsoft.Extensions.Options;
using System.Text;

namespace Conways_GameOfLife_API.Services
{
    public class BoardService
    {
        private readonly BoardRepository _boardRepository;
        private readonly GameOfLifeService _gameService; 
        private readonly APIConfig _config;
        private readonly ILogger<BoardService> _logger;

        public BoardService(BoardRepository inMemoryStore, GameOfLifeService gameService, IOptions<APIConfig> config, ILogger<BoardService> logger)
        {
            _boardRepository = inMemoryStore;
            _gameService = gameService;
            _config = config.Value;
            _logger = logger;
        }

        public async Task<Guid> AddBoardAsync(bool[,] board)
        {
            var id = await _boardRepository.AddBoardAsync(board);

            _logger.LogInformation("Board created with id: {id}", id);

            return id;
        }

        public async Task<bool[,]?> GetNextAsync(Guid id)
        {
            var board = await _boardRepository.GetBoardAsync(id);

            if (board == null)
            {
                _logger.LogWarning("Board not found: {BoardId}", id);
                return null;
            }

            var next = _gameService.GetNextState(board);
            await _boardRepository.UpdateBoardAsync(id, next);
            return next;
        }

        public async Task<bool[,]?> AdvanceAsync(Guid id, int steps)
        {
            var board = await _boardRepository.GetBoardAsync(id);
            if (board == null)
            {
                _logger.LogWarning("Board not found: {BoardId}", id);
                return null;
            }

            for (int i = 0; i < steps; i++)
                board = _gameService.GetNextState(board);

            _logger.LogInformation("Board with id: {id} advanced {steps} steps", id, steps);

            await _boardRepository.UpdateBoardAsync(id, board);
            return board;
        }

        public async Task<(bool[,]? state, bool success, string reason)> GetFinalAsync(Guid id)
        {
            var board = await _boardRepository.GetBoardAsync(id);
            if (board == null)
            {
                _logger.LogWarning("Board not found: {BoardId}", id);
                return (null, false, "Board was not found");
            }

            var seen = new HashSet<string>();
            for (int i = 0; i < _config.MaxIterations; i++)
            {
                var boardString = ConvertBoardToString(board);
                if (!seen.Add(boardString))
                {
                    _logger.LogInformation("Oscillation detected at iteration {Iter} for board {BoardId}", i, id);
                    return (board, true, "Reached simulation oscillation state");
                }

                var next = _gameService.GetNextState(board);
                if (_gameService.BoardsEqual(board, next))
                {
                    _logger.LogInformation("Static state reached at iteration {Iter} for board {BoardId}", i, id);
                    return (next, true, "Reached simulation static state"); 
                }

                board = next;
            }

            _logger.LogWarning("Max iterations ({MaxIteractions}) exceeded without reaching final state for board {BoardId}", _config.MaxIterations, id);
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
