namespace Application.DTOs.ContatoDtos
{
    public class AtualizarContatoRequest
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? Telefone { get; set; }
        public int? CodigoArea { get; set; }
    }
}
