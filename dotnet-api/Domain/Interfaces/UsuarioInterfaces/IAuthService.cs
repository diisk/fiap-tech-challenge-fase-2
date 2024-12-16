using Domain.Entities;

namespace Domain.Interfaces.UsuarioInterfaces
{
    public interface IAuthService
    {
        string logar(string login, string senha);
        Usuario registrar(Usuario usuario);
        Usuario? GetUsuarioLogado();
    }
}
