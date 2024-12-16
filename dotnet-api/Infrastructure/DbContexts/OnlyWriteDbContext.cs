using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
    public class OnlyWriteDbContext: DbContext
    {

        public OnlyWriteDbContext(DbContextOptions<OnlyWriteDbContext> options):base(options) {}
        public DbSet<Usuario> UsuarioSet { get; set; }
        public DbSet<Area> AreaSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlyReadDbContext).Assembly);
        }
    }
}
