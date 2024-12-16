using Application.DTOs;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GetToken(TokenData data);
        string GetIdentifierFromClaimsPrincipal(ClaimsPrincipal user);
    }
}
