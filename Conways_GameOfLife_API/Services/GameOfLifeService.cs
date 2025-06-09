using Conways_GameOfLife_API.Data;

namespace Conways_GameOfLife_API.Services
{
    public class GameOfLifeService
    {
        private readonly InMemoryBoardStore _boardStore;

        public GameOfLifeService(InMemoryBoardStore boardStore)
        {
            _boardStore = boardStore;
        }

        public Guid AddBoard(bool[,] board)
        {
            return _boardStore.AddBoard(board);
        }
    }
}
