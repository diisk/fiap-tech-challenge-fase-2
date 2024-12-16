using Domain.Entities;

namespace Domain.Interfaces.ContatoInterfaces
{
    public interface IContatoRepository : IRepository<Contato>
    {
        Contato? FindByCodigoAreaAndTelefone(int codigoArea, int telefone);
        List<Contato> FindByCodigoArea(int codigoArea);
    }
}
