using Domain.Entities;
using Domain.Interfaces.ContatoInterfaces;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext) : base(onlyWriteDbContext, onlyReadDbContext)
        {
        }

        public List<Contato> FindByCodigoArea(int codigoArea)
        {
            return onlyReadDbSet.Where(c => !c.Removed && c.Area.Codigo == codigoArea).ToList();
        }

        public Contato? FindByCodigoAreaAndTelefone(int codigoArea, int telefone)
        {
            return onlyReadDbSet.FirstOrDefault(
                c => c.Telefone == telefone
                && c.CodigoArea == codigoArea
                && !c.Removed);
        }
    }
}
