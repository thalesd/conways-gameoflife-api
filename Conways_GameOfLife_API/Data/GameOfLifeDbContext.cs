using Conways_GameOfLife_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conways_GameOfLife_API.Data
{
    public class GameOfLifeDbContext : DbContext
    {
        public DbSet<BoardEntity> Boards { get; set; }

        public GameOfLifeDbContext(DbContextOptions<GameOfLifeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardEntity>().HasKey(b => b.Id);
        }
    }
}
