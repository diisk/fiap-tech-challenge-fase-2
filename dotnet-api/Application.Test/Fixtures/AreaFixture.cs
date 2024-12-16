using Domain.Entities;
using Domain.Enums.AreaEnums;

namespace Application.Test.Fixtures
{
    public class AreaFixture
    {
        public Area AreaValida
        {
            get
            {
                return new Area
                {
                    Codigo = 31,
                    Regiao = RegiaoBrasil.SUDESTE,
                    SiglaEstado = "MG",
                    Cidades = "Belo Horizonte;Contagem;Betim;Nova Lima",
                    Descricao = "Teste"
                };
            }
        }

    }
}
