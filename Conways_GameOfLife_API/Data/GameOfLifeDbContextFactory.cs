using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Conways_GameOfLife_API.Data
{
    public class GameOfLifeDbContextFactory : IDesignTimeDbContextFactory<GameOfLifeDbContext>
    {
        public GameOfLifeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameOfLifeDbContext>();

            // Hardcoded fallback or use ENV var directly here
            var connectionString = Environment.GetEnvironmentVariable("DB_DATA_SOURCE")
                ?? "Data Source=gameOfLife.db";

            optionsBuilder.UseSqlite(connectionString);

            return new GameOfLifeDbContext(optionsBuilder.Options);
        }
    }
}
