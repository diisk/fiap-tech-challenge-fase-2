using Application.DTOs;
using Application.DTOs.Auth;
using Application.DTOs.AuthDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.UsuarioInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IAuthService authService;
        private readonly IMapper mapper;
        private readonly IResponseService responseService;
        private readonly ICryptoService cryptoService;

        public UsuarioController(IAuthService authService, IMapper mapper, IResponseService responseService, ICryptoService cryptoService)
        {
            this.authService = authService;
            this.mapper = mapper;
            this.responseService = responseService;
            this.cryptoService = cryptoService;
        }

        /// <summary>
        /// Gerar uma token para fazer a autenticação do usuário.
        /// </summary>
        /// <param name="request">Dados para gerar uma token.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na geração da token.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="401">Não foi possível autenticar o usuário com os dados fornecidos.</response>
        [HttpPost("logar")]
        [AllowAnonymous]
        public ActionResult<BaseResponse<LogarResponse>> login([FromBody] LogarRequest request)
        {
            authService.GetUsuarioLogado();
            var token = authService.logar(request.Login, request.Senha);
            return responseService.Ok(new LogarResponse { Token = token });
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema
        /// </summary>
        /// <param name="request">Dados para cadastrar um usuário.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do usuário.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="409">O login informado não está disponível.</response>
        [HttpPost("registrar")]
        [AllowAnonymous]
        public ActionResult<BaseResponse<RegistrarResponse>> registrar([FromBody] RegistrarRequest request)
        {
            var usuario = mapper.Map<Usuario>(request);
            var retorno = authService.registrar(usuario);
            return responseService.Ok(new RegistrarResponse { Id = retorno.ID});
        }
    }
}
