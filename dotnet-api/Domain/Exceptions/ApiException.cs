using System.Net;

namespace Application.Exceptions
{
    public abstract class ApiException : Exception
    {
        public HttpStatusCode Status { get; set; }
        public ApiException(HttpStatusCode status, string mensagem) : base(mensagem)
        {
            Status = status;
        }
    }
}
