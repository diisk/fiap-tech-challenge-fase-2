using Application.DTOs.AreaDtos;

namespace Application.DTOs.ContatoDtos
{
    public class ContatoResponse
    {
        public int ID { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required int Telefone { get; set; }
        public required AreaResponse Area { get; set; }
    }
}
