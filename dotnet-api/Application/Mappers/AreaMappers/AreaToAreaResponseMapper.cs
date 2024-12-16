using Application.DTOs.AreaDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.AreaInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Mappers.AreaMappers
{
    public class AreaToAreaResponseMapper : CustomMapper<Area, AreaResponse>
    {
        private readonly IAreaService areaService;
        private readonly IMapper mapper;

        public AreaToAreaResponseMapper(IMapper mapper, IAreaService areaService) : base(mapper)
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
            cfg.CreateMap<Area, AreaResponse>().ForMember(
                    dest => dest.Cidades,
                    opt => opt.MapFrom(
                        src => ConverteCidades(src.Cidades)
                        )
                );
            services.AddScoped<AreaToAreaResponseMapper>();
        }

    }
}
