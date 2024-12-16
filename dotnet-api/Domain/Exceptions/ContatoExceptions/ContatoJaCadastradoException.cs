using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.ContatoExceptions
{
    public class ContatoJaCadastradoException : ApiException
    {
        public ContatoJaCadastradoException() : base(HttpStatusCode.Conflict, "Já existe um contato cadastrado com esse telefone e código de área.")
        {
        }
    }
}
