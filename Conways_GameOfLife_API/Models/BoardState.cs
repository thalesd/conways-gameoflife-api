namespace Conways_GameOfLife_API.Models
{
    public class BoardState
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool[,] GridSpace { get; set; }
    }
}
