using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Application.Test.Fixtures;
using Domain.Entities;
using Domain.Exceptions.AuthExceptions;
using Domain.Interfaces.UsuarioInterfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Application.Test.Tests
{
    [Trait("Category", "Unit")]
    public class AuthServiceTest : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;

        public AuthServiceTest(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void RegistrarUsuario_QuandoLoginInvalido_DeveLancarExcecao(string login)
        {
            //GIVEN
            var mockRepository = new Mock<IUsuarioRepository>();
            var mockCryptoService = new Mock<ICryptoService>();
            var mockTokenService = new Mock<ITokenService>();
            var mockHttpContextAcessor = new Mock<IHttpContextAccessor>();

            Usuario usuario = fixture.UsuarioValido;
            usuario.Login = login;

            var authService = new AuthService(mockRepository.Object, mockCryptoService.Object, mockTokenService.Object,mockHttpContextAcessor.Object);

            //WHEN & THEN
            Assert.Throws<ValidacaoException>(() => authService.registrar(usuario));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void RegistrarUsuario_QuandoSenhaInvalida_DeveLancarExcecao(string senha)
        {
            //GIVEN
            var mockRepository = new Mock<IUsuarioRepository>();
            var mockCryptoService = new Mock<ICryptoService>();
            var mockTokenService = new Mock<ITokenService>();
            var mockHttpContextAcessor = new Mock<IHttpContextAccessor>();

            Usuario usuario = fixture.UsuarioValido;
            usuario.Senha = senha;

            var authService = new AuthService(mockRepository.Object, mockCryptoService.Object, mockTokenService.Object, mockHttpContextAcessor.Object);

            //WHEN & THEN
            Assert.Throws<ValidacaoException>(() => authService.registrar(usuario));
        }

        [Fact]
        public void RegistrarUsuario_QuandoDadosCorretos_DeveRetornarNovoUsuario()
        {
            //GIVEN
            var mockRepository = new Mock<IUsuarioRepository>();
            var mockCryptoService = new Mock<ICryptoService>();
            var mockTokenService = new Mock<ITokenService>();
            var mockHttpContextAcessor = new Mock<IHttpContextAccessor>();

            Usuario usuario = fixture.UsuarioValido;
            Usuario usuarioRetorno = fixture.UsuarioValido;
            usuarioRetorno.ID = 1;

            mockRepository.Setup(repo => repo.Save(usuario)).Returns(usuarioRetorno);

            var authService = new AuthService(mockRepository.Object, mockCryptoService.Object, mockTokenService.Object, mockHttpContextAcessor.Object);

            //WHEN
            var usuarioCadastrado = authService.registrar(usuario);

            //THEN
            Assert.NotNull(usuarioCadastrado);
            Assert.Equal(usuarioCadastrado.ID, usuarioRetorno.ID);
        }

        [Fact]
        public void RegistrarUsuario_QuandoLoginJaExiste_DeveLancarExecao()
        {
            //GIVEN
            var mockRepository = new Mock<IUsuarioRepository>();
            var mockCryptoService = new Mock<ICryptoService>();
            var mockTokenService = new Mock<ITokenService>();
            var mockHttpContextAcessor = new Mock<IHttpContextAccessor>();

            Usuario usuario = fixture.UsuarioValido;
            Usuario usuarioRetorno = fixture.UsuarioValido;
            usuarioRetorno.ID = 1;

            mockRepository.Setup(repo => repo.FindByLogin(usuario.Login)).Returns(usuarioRetorno);

            var authService = new AuthService(mockRepository.Object, mockCryptoService.Object, mockTokenService.Object, mockHttpContextAcessor.Object);

            //WHEN & THEN
            Assert.Throws<LoginIndisponivelException>(() => authService.registrar(usuario));
        }

        [Theory]
        [InlineData(null,"senha",true)]
        [InlineData("","senha",true)]
        [InlineData("login",null,true)]
        [InlineData("login","",true)]
        [InlineData("loginerrado","senha",true)]
        [InlineData("login","senhaerrada",true)]
        [InlineData("login","senha",false)]
        public void Logar_SeDadosIncorretos_DeveLancarExecao(string login, string senha, bool deveLancarExcecao)
        {
            //GIVEN
            var mockRepository = new Mock<IUsuarioRepository>();
            var mockCryptoService = new Mock<ICryptoService>();
            var mockTokenService = new Mock<ITokenService>();
            var mockHttpContextAcessor = new Mock<IHttpContextAccessor>();

            var token = "token";

            Usuario usuario = fixture.UsuarioValido;
            usuario.ID = 1;
            usuario.Login = "login";

            mockRepository.Setup(repo => repo.FindByLogin(usuario.Login)).Returns(usuario);
            mockCryptoService.Setup(repo => repo.VerificarSenhaHasheada("senha", usuario.Senha)).Returns(true);
            mockTokenService.Setup(repo => repo.GetToken(It.Is<TokenData>(td=>td.Identifier==usuario.ID.ToString()))).Returns(token);


            var authService = new AuthService(mockRepository.Object, mockCryptoService.Object, mockTokenService.Object, mockHttpContextAcessor.Object); 

            if (deveLancarExcecao)
            {
                //WHEN & THEN
                Assert.Throws<DadosIncorretosException>(() => authService.logar(login,senha));
                return;
            }
            else
            {
                //WHEN
                var responseToken = authService.logar(login,senha);

                //THEN
                Assert.Equal(responseToken,token);
            }
            
        }

    }
}