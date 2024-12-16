using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.DbContexts
{
    public class BaseDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            var options = DbContextOptionsConfigurator.Create<T>("API");
            return (T)Activator.CreateInstance(typeof(T), options)!;
        }
    }
}
