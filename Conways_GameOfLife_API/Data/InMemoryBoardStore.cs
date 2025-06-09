namespace Conways_GameOfLife_API.Data
{
    public class InMemoryBoardStore
    {
        private readonly Dictionary<Guid, bool[,]> _inMemoryBoards = new();

        public Guid AddBoard(bool[,] gridState)
        {
            var id = Guid.NewGuid();
            _inMemoryBoards[id] = gridState;
            return id;
        }
    }
}
