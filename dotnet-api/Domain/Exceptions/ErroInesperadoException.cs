using System.Net;

namespace Application.Exceptions
{
    public class ErroInesperadoException : ApiException
    {
        public ErroInesperadoException(string message) : base(HttpStatusCode.InternalServerError, message) { }

    }
}
