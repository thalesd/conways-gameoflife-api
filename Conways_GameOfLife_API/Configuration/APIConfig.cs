namespace Conways_GameOfLife_API.Configuration
{
    public class APIConfig
    {
        public int MaxIterations { get; set; } = 1000;

        public string DataSource { get; set; } = "Data Source=gameOfLife.db";
    }
}
