using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions.AuthExceptions;
using Domain.Interfaces.UsuarioInterfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ICryptoService cryptoService;
        private readonly ITokenService tokenService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(IUsuarioRepository usuarioRepository, ICryptoService cryptoService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            this.usuarioRepository = usuarioRepository;
            this.cryptoService = cryptoService;
            this.tokenService = tokenService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Usuario? GetUsuarioLogado()
        {
            var user = httpContextAccessor.HttpContext.User;
            if (user.Identity is { IsAuthenticated: false })
            {
                return null;
            }
            var userId = Convert.ToInt32(tokenService.GetIdentifierFromClaimsPrincipal(user));
            var retorno = usuarioRepository.FindById(userId);

            if (retorno == null)
            {
                throw new ErroInesperadoException("Usuário não encontrado.");
            }

            return retorno;
        }

        public string logar(string login, string senha)
        {
            Usuario? user = usuarioRepository.FindByLogin(login);
            if (user != null && cryptoService.VerificarSenhaHasheada(senha, user.Senha))
            {
                return tokenService.GetToken(new TokenData { Identifier = user.ID.ToString() });
            }
            throw new DadosIncorretosException();
        }

        public Usuario registrar(Usuario usuario)
        {
            usuario.Validate();
            usuario.Senha = cryptoService.HashearSenha(usuario.Senha);
            Usuario? user = usuarioRepository.FindByLogin(usuario.Login);
            if (user != null)
                throw new LoginIndisponivelException();

            return usuarioRepository.Save(usuario);
        }
    }
}
