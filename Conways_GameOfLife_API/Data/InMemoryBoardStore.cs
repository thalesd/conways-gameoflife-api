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

        public bool[,]? GetBoard(Guid id)
        {
            return _inMemoryBoards.TryGetValue(id, out var board) ? board : null;
        }

        public void UpdateBoard(Guid id, bool[,] grid)
        {
            if (_inMemoryBoards.ContainsKey(id))
                _inMemoryBoards[id] = grid;
        }
    }
}
