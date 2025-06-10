using Conways_GameOfLife_API.Entities;
using Conways_GameOfLife_API.Helpers;
using System.Text.Json;

namespace Conways_GameOfLife_API.Data
{
    public class BoardRepository
    {
        private readonly GameOfLifeDbContext _context;

        public BoardRepository(GameOfLifeDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddBoardAsync(bool[,] board)
        {
            var entity = new BoardEntity
            {
                Id = Guid.NewGuid(),
                GridJson = JsonSerializer.Serialize(ArrayHelper.ToJaggedArray(board))
            };

            _context.Boards.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool[,]?> GetBoardAsync(Guid id)
        {
            var entity = await _context.Boards.FindAsync(id);
            if (entity == null) return null;

            var jagged = JsonSerializer.Deserialize<bool[][]>(entity.GridJson);
            return ArrayHelper.To2DArray(jagged);
        }

        public async Task UpdateBoardAsync(Guid id, bool[,] board)
        {
            var entity = await _context.Boards.FindAsync(id);
            if (entity == null) return;

            entity.GridJson = JsonSerializer.Serialize(ArrayHelper.ToJaggedArray(board));
            await _context.SaveChangesAsync();
        }
    }
}
