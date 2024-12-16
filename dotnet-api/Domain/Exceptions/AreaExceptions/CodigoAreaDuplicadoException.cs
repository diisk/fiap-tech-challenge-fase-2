using Application.Exceptions;
using System.Net;

namespace Domain.Exceptions.AreaExceptions
{
    public class CodigoAreaDuplicadoException : ApiException
    {
        public CodigoAreaDuplicadoException() : base(
            HttpStatusCode.Conflict,
            "Existem códigos de área duplicados na lista fornecida.")
        {
        }
    }
}
