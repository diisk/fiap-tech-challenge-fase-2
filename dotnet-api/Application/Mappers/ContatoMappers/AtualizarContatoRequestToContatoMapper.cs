using Application.DTOs.ContatoDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.AreaInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Mappers.ContatoMappers
{
    public class AtualizarContatoRequestToContatoMapper : CustomMapper<AtualizarContatoRequest, Contato>
    {
        private readonly IAreaService areaService;
        private readonly IMapper mapper;


        public AtualizarContatoRequestToContatoMapper(IMapper mapper, IAreaService areaService) : base(mapper)
        {
            this.areaService = areaService;
            this.mapper = mapper;
        }

        private static List<string> ConverteCidades(string? cidades)
        {
            return string.IsNullOrEmpty(cidades) ? new() : cidades.Split(';').ToList();
        }

        public static void ConfigureMapping(IMapperConfigurationExpression cfg, IServiceCollection services)
        {
            cfg.CreateMap<AtualizarContatoRequest, Contato>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) =>
                {
                    if (srcMember == null) return false;
                    if (srcMember.GetType().IsValueType && srcMember.Equals(Activator.CreateInstance(srcMember.GetType())))
                    {
                        return false;
                    }
                    return true;
                }));
            services.AddScoped<AtualizarContatoRequestToContatoMapper>();
        }


        protected override void ApplyCustomMappings(AtualizarContatoRequest source, Contato target)
        {
            if (source.CodigoArea.HasValue)
            {
                var area = areaService.BuscarPorCodigoArea(source.CodigoArea.Value);
                target.Area = area;
            }
            
        }


    }
}
