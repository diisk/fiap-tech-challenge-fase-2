using Domain.Entities;
using Domain.Exceptions.ContatoExceptions;
using Domain.Interfaces.AreaInterfaces;
using Domain.Interfaces.ContatoInterfaces;

namespace Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository contatoRepository;
        private readonly IAreaService areaService;

        public ContatoService(IContatoRepository contatoRepository, IAreaService areaService)
        {
            this.contatoRepository = contatoRepository;
            this.areaService = areaService;
        }

        public Contato AtualizarContato(Contato contato)
        {

            contato.Validate();
            ValidaSeContatoExiste(contato.ID);

            return contatoRepository.Save(contato);
        }

        private void ValidaSeContatoExiste(int id)
        {
            BuscarPorId(id);
        }

        public Contato BuscarPorId(int id)
        {
            var dbContato = contatoRepository.FindById(id);
            if (dbContato == null) throw new ContatoNaoEncontradoException();
            return dbContato;
        }

        public Contato CadastrarContato(Contato contato)
        {
            contato.Validate();
            var dbContato = contatoRepository.FindByCodigoAreaAndTelefone(contato.CodigoArea,contato.Telefone);
            if (dbContato != null)
                throw new ContatoJaCadastradoException();

            Area area = areaService.BuscarPorCodigoArea(contato.CodigoArea);

            contatoRepository.Save(contato);
            contato.Area = area;
            return contato;
        }

        public void ExcluirContato(int id)
        {
            var contato = contatoRepository.FindById(id);
            if (contato != null)
            {
                contato.Remove();
                contatoRepository.Save(contato);
            }
            
        }

        public List<Contato> ListarContatos(int? codigoArea = null)
        {
            if (codigoArea == null) return contatoRepository.FindAll().ToList();

            return contatoRepository.FindByCodigoArea(codigoArea.Value);
        }
    }
}
