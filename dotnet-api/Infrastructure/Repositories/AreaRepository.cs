using Domain.Entities;
using Domain.Interfaces.AreaInterfaces;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories
{
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        public AreaRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext) : base(onlyWriteDbContext, onlyReadDbContext)
        {
        }

        public List<Area> FindByCodigo(List<int> codigos)
        {
            return onlyReadDbSet.Where(x => codigos.Contains(x.Codigo)).ToList();
        }

    }
}
