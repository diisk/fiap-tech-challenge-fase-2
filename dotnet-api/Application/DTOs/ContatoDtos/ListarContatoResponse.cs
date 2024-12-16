namespace Application.DTOs.ContatoDtos
{
    public class ListarContatoResponse
    {
        public int TotalResultados {  get; set; }
        public required List<ContatoResponse> Resultados { get; set; }
    }
}
