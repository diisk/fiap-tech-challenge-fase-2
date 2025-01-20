using Application.DTOs;
using Application.DTOs.AreaDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.AreaInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/areas")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IResponseService responseService;
        private readonly IAreaService areaService;

        public AreaController(IMapper mapper, IResponseService responseService, IAreaService areaService)
        {
            this.mapper = mapper;
            this.responseService = responseService;
            this.areaService = areaService;
        }

        /// <summary>
        /// Cadastra uma ou mais áreas associadas a uma região.
        /// </summary>
        /// <param name="request">Dados para cadastrar uma ou mais áreas.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro das áreas.</response>
        /// <response code="409">Uma ou mais áreas informadas já estão cadastradas.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        [HttpPost]
        public ActionResult<BaseResponse<List<AreaResponse>>> CadastrarArea([FromBody] CadastrarAreaRequest request)
        {
            var areas = mapper.Map<List<Area>>(request.Areas);
            var retorno = areaService.CadastrarAreas(areas);
            var response = mapper.Map<List<AreaResponse>>(retorno);
            return responseService.Ok(response);
        }
    }
}
