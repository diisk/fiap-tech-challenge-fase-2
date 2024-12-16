using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.ContatoExceptions
{
    public class ContatoNaoEncontradoException : ApiException
    {
        public ContatoNaoEncontradoException() : base(HttpStatusCode.NotFound, "Contato não encontrado.")
        {
        }
    }
}
