using Domain.Enums.AreaEnums;

namespace Application.DTOs.AreaDtos
{
    public class NovaAreaRequest
    {
        public required int Codigo { get; set; }
        public required RegiaoBrasil Regiao { get; set; }

        public required string SiglaEstado { get; set; }

        public string? Cidades { get; set; }

        public string? Descricao { get; set; }
    }
}
