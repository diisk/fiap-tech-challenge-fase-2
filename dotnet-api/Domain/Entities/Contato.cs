using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Contatos")]
    public class Contato:EntityBase
    {
        [Required]
        public required string Nome { get; set; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "O endereço de e-mail não é válido.")]
        //[EmailAddress(ErrorMessage = "O endereço de email não é válido.")] não funcionou em todos os casos
        public required string Email { get; set; }
        [Required]
        [Range(10000000,999999999, ErrorMessage = "O número deve conter entre 8 e 9 dígitos.")]
        public required int Telefone { get; set; }
        [Required]
        [Range(10,99, ErrorMessage = "O código de área deve conter 2 dígitos.")]
        public required int CodigoArea { get; set; }
        public virtual Area? Area { get; set; }
    }
}
