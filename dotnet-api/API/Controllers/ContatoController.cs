using Application.DTOs;
using Application.DTOs.ContatoDtos;
using Application.Interfaces;
using Application.Mappers.ContatoMappers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.ContatoInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/contatos")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IResponseService responseService;
        private readonly IContatoService contatoService;
        private readonly AtualizarContatoRequestToContatoMapper atualizarContatoRequestToContatoMapper;

        public ContatoController(IMapper mapper, IResponseService responseService, IContatoService contatoService, AtualizarContatoRequestToContatoMapper atualizarContatoRequestToContatoMapper)
        {
            this.mapper = mapper;
            this.responseService = responseService;
            this.contatoService = contatoService;
            this.atualizarContatoRequestToContatoMapper = atualizarContatoRequestToContatoMapper;
        }

        /// <summary>
        /// Cadastra um novo contato no sistema.
        /// </summary>
        /// <param name="request">Dados do contato a ser cadastrado.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do contato.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="409">O contato informado já está cadastrado.</response>
        [HttpPost]
        public ActionResult<BaseResponse<ContatoResponse>> CadastrarContato([FromBody] CadastrarContatoRequest request)
        {
            var contato = mapper.Map<Contato>(request);
            var retorno = contatoService.CadastrarContato(contato);
            var response = mapper.Map<ContatoResponse>(retorno);
            return responseService.Ok(response);
        }

        /// <summary>
        /// Atualiza as informações de um contato existente no sistema.
        /// </summary>
        /// <param name="id">Identificador do contato a ser atualizado.</param>
        /// <param name="request">Dados atualizados do contato.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na atualização do contato.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="404">Contato não encontrado.</response>
        /// <response code="409">Conflito ao atualizar o contato.</response>
        [HttpPatch("{id}")]
        public ActionResult<BaseResponse<ContatoResponse>> AtualizarContato([FromRoute] int id, [FromBody] AtualizarContatoRequest request)
        {
            var contato = contatoService.BuscarPorId(id);
            atualizarContatoRequestToContatoMapper.Map(request, contato);
            var retorno = contatoService.AtualizarContato(contato);
            var response = mapper.Map<ContatoResponse>(retorno);
            return responseService.Ok(response);
        }

        /// <summary>
        /// Lista todos os contatos cadastrados, com a possibilidade de filtrar por código de área.
        /// </summary>
        /// <param name="codigoArea">Filtro opcional para o código de área dos contatos.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na listagem dos contatos.</response>
        /// <response code="400">Parâmetros da requisição inválidos.</response>
        [HttpGet]
        public ActionResult<BaseResponse<ListarContatoResponse>> ListarContatos([FromQuery] int? codigoArea)
        {
            var retorno = contatoService.ListarContatos(codigoArea);
            var response = new ListarContatoResponse
            {
                TotalResultados = retorno.Count,
                Resultados = mapper.Map<List<ContatoResponse>>(retorno)
            };
            return responseService.Ok(response);
        }

        /// <summary>
        /// Exclui um contato do sistema com base no seu identificador.
        /// </summary>
        /// <param name="id">Identificador do contato a ser excluído.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na exclusão do contato.</response>
        /// <response code="400">Parâmetros da requisição inválidos.</response>
        [HttpDelete("{id}")]
        public ActionResult<BaseResponse<Object>> ExcluirContato([FromRoute] int id)
        {
            contatoService.ExcluirContato(id);
            return responseService.Ok<Object>();
        }
    }
}
