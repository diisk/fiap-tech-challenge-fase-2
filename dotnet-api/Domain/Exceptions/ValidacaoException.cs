using System.Net;

namespace Application.Exceptions
{
    public class ValidacaoException : ApiException
    {
        public ValidacaoException(string mensagem) : base(HttpStatusCode.BadRequest, mensagem) { }

    }
}
