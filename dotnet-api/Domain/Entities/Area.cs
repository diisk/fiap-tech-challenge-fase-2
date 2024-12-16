using Domain.Enums.AreaEnums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Areas")]
    public class Area : EntityBase
    {
        [Range(10,99, ErrorMessage = "O código de área deve conter 2 números")]
        public required int Codigo { get; set; }
        [EnumDataType(typeof(RegiaoBrasil), ErrorMessage = "Região inválida.")]
        public required RegiaoBrasil Regiao {  get; set; }

        [Required]
        [Length(2,2,ErrorMessage = "A sigla deve conter 2 letras.")]
        public required string SiglaEstado { get; set; }

        public string? Cidades { get; set; }

        public string? Descricao { get; set; }
    }
}
