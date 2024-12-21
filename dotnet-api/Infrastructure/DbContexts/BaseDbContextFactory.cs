using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.DbContexts
{
    public abstract class BaseDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            var options = DbContextOptionsConfigurator.Create<T>();
            return (T)Activator.CreateInstance(typeof(T), options)!;
        }
    }
}
