using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.AreaExceptions
{
    public class CodigoAreaNaoCadastradoException : ApiException
    {
        public CodigoAreaNaoCadastradoException() : base(HttpStatusCode.NotFound, "Esse código de área não está cadastrado ou não existe.")
        {
        }
    }
}
