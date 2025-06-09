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

        public static bool[][] ToJaggedArray(bool[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            var result = new bool[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new bool[cols];
                for (int j = 0; j < cols; j++)
                    result[i][j] = array[i, j];
            }

            return result;
        }
    }
}
