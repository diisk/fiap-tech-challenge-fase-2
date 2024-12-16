using Domain.Entities;

namespace Domain.Interfaces.AreaInterfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        List<Area> FindByCodigo(List<int> codigos);
    }
}
