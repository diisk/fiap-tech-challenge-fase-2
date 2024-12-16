using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.AuthExceptions
{
    public class DadosIncorretosException : ApiException
    {
        public DadosIncorretosException() : base(HttpStatusCode.Unauthorized, "Dados incorretos.") { }
    }
}
