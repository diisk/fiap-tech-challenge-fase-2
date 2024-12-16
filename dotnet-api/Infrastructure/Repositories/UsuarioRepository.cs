using Domain.Entities;
using Domain.Interfaces.UsuarioInterfaces;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext) : base(onlyWriteDbContext, onlyReadDbContext)
        {
        }

        public Usuario? FindByLogin(string login)
        {
            return onlyReadDbSet.FirstOrDefault(x => x.Login == login);
        }
    }
}
