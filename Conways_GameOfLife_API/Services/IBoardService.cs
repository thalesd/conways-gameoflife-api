namespace Conways_GameOfLife_API.Services
{
    public interface IBoardService
    {
        Task<Guid> AddBoardAsync(bool[,] board);
        Task<bool[,]?> GetNextAsync(Guid id);
        Task<bool[,]?> AdvanceAsync(Guid id, int steps);
        Task<(bool[,]? state, bool success, string reason)> GetFinalAsync(Guid id);
    }
}