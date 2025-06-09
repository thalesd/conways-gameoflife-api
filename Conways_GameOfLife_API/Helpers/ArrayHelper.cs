namespace Conways_GameOfLife_API.Helpers
{
    public class ArrayHelper
    {
        public static bool[,] To2DArray(bool[][] jagged)
        {
            int rows = jagged.Length;
            int cols = jagged[0].Length;
            var result = new bool[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = jagged[i][j];

            return result;
        }
    }
}
