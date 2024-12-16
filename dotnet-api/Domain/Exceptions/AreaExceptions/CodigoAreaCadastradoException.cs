using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.AreaExceptions
{
    public class CodigoAreaCadastradoException : ApiException
    {
        public CodigoAreaCadastradoException(List<int> codigos) : base(
            HttpStatusCode.Conflict,
            "Os seguintes códigos de área já estão cadastrados: " + CodigosToString(codigos))
        {
        }

        private static string CodigosToString(List<int> codigos)
        {
            return string.Join(',', codigos.Select(c => c.ToString()));
        }
    }
}
