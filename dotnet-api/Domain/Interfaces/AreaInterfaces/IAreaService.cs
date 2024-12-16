using Domain.Entities;

namespace Domain.Interfaces.AreaInterfaces
{
    public interface IAreaService
    {
        List<Area> CadastrarAreas(List<Area> areas);
        Area BuscarPorCodigoArea(int codigoArea);
    }
}
