using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.AuthExceptions
{
    public class LoginIndisponivelException : ApiException
    {
        public LoginIndisponivelException() : base(HttpStatusCode.Conflict, "Esse login não está disponível.") { }
    }
}
