using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Context
{
    internal sealed class DesignTimeContextFactory : IDesignTimeDbContextFactory<CinemaContext>
    {
        CinemaContext IDesignTimeDbContextFactory<CinemaContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinemaContext>();
            optionsBuilder.UseSqlServer("Server=; Database=;Trusted_Connection=True;MultipleActiveResultSets=True");
            return new CinemaContext(optionsBuilder.Options);
        }
    }
}
