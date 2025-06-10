namespace Conways_GameOfLife_API.Data
{
    public interface IBoardRepository
    {
        Task<Guid> AddBoardAsync(bool[,] board);
        Task<bool[,]?> GetBoardAsync(Guid id);
        Task UpdateBoardAsync(Guid id, bool[,] board);
    }
}