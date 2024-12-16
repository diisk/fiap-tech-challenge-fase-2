using Application.Exceptions;
using Domain.Entities;
using Domain.Exceptions.AreaExceptions;
using Domain.Interfaces.AreaInterfaces;

namespace Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public Area BuscarPorCodigoArea(int codigoArea)
        {
            var area = areaRepository.FindByCodigo([codigoArea]);
            if (area.Count==0) throw new CodigoAreaNaoCadastradoException();

            return area.First();
        }

        public List<Area> CadastrarAreas(List<Area> areas)
        {
            if (areas.Count == 0)
            {
                throw new ConteudoDiferenteException();
            }

            List<int> codigos = new List<int>();
            foreach (Area area in areas) {
                area.Validate();

                if (codigos.Contains(area.Codigo))
                    throw new CodigoAreaDuplicadoException();

                codigos.Add(area.Codigo);
            }

            if (areaRepository.FindByCodigo(codigos).Count > 0)
                throw new CodigoAreaCadastradoException(codigos);

            return areaRepository.SaveAll(areas);
        }
    }
}
