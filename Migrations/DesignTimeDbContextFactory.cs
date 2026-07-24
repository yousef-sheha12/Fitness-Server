using Fitness.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fitness.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Fitness_DB;Trusted_Connection=True;TrustServerCertificate=True");
            return new AppDbContext(builder.Options);
        }
    }
}
