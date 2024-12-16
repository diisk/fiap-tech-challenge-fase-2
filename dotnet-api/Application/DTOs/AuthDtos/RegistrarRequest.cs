namespace Application.DTOs.Auth
{
    public class RegistrarRequest
    {
        public required string Login { get; set; }
        public required string Senha { get; set; }
    }
}