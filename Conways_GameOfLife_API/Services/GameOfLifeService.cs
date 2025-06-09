namespace Conways_GameOfLife_API.Services
{
    public class GameOfLifeService
    {
        public bool[,] GetNextState(bool[,] current)
        {
            int rows = current.GetLength(0);
            int cols = current.GetLength(1);
            var next = new bool[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int aliveNeighbors = CountNeighborsAlive(current, r, c);
                    bool isAlive = current[r, c];

                    next[r, c] = isAlive
                        ? aliveNeighbors == 2 || aliveNeighbors == 3
                        : aliveNeighbors == 3;
                }
            }

            return next;
        }

        private int CountNeighborsAlive(bool[,] grid, int x, int y)
        {
            int count = 0;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    int nx = x + dx, ny = y + dy;
                    if (nx >= 0 && ny >= 0 && nx < rows && ny < cols && grid[nx, ny])
                        count++;
                }

            return count;
        }

        public bool BoardsEqual(bool[,] a, bool[,] b)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (a[r, c] != b[r, c]) return false;
            return true;
        }
    }
}
