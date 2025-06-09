using Conways_GameOfLife_API.Data;
using System.Text;

namespace Conways_GameOfLife_API.Services
{
    public class BoardService
    {
        private readonly InMemoryBoardStore _inMemoryStore;
        private readonly GameOfLifeService _gameService;

        public BoardService(InMemoryBoardStore inMemoryStore, GameOfLifeService gameService)
        {
            _inMemoryStore = inMemoryStore;
            _gameService = gameService;
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
    }
}
