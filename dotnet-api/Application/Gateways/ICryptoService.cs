namespace Application.Interfaces
{
    public interface ICryptoService
    {
        public string HashearSenha(string senha);
        public string CriptografarString(string texto, string chave);
        public string DescriptografarString(string textoCriptografado, string chave);
        public bool VerificarSenhaHasheada(string senha, string senhaHasheada);
    }
}
