using Domain.Entities;

namespace Domain.Interfaces.UsuarioInterfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario? FindByLogin(string email);
    }
}
