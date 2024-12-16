using Domain.Entities;
using Domain.Enums.AreaEnums;

namespace Application.Test.Fixtures
{
    public class ContatoFixture
    {
        public Contato ContatoValido
        {
            get
            {
                return new Contato
                {
                    CodigoArea = 31,
                    Email="joao@teste.com",
                    Area = new Area
                    {
                        Codigo = 31,
                        Regiao = RegiaoBrasil.SUDESTE,
                        SiglaEstado = "MG",
                        Cidades = "Belo Horizonte;Contagem;Betim;Nova Lima",
                        Descricao = "Teste"
                    },
                    Nome = "Joao Teste da Silva",
                    Telefone = 970707070,
                };
            }
        }

        public List<Contato> ListaContatosValidos { get
            {
                var maxTelefone = 999999999;
                var contatos = new List<Contato>();
                for (int i = 0; i < 5; i++)
                {
                    var contato = ContatoValido;
                    contato.Telefone = maxTelefone - i;
                    contato.Area.Codigo = 31;
                    contatos.Add(contato);
                }
                for (int i = 0; i < 5; i++)
                {
                    var contato = ContatoValido;
                    contato.Telefone = maxTelefone - i;
                    contato.Area.Codigo = 32;
                    contatos.Add(contato);
                }

                return contatos;
            } }

    }
}
