using Domain.Entities;

namespace Application.Test.Fixtures
{
    public class UsuarioFixture
    {
        public Usuario UsuarioValido
        {
            get
            {
                return new Usuario
                {
                    Login = "teste",
                    Senha = "senha"
                };
            }
        }
    }
}
