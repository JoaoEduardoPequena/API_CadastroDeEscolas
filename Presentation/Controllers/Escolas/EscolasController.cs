using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.App_Escolas.UseCases.Escolas.Command.Criar;
using Application.App_Escolas.UseCases.Escolas.Command.Editar;
using Application.App_Escolas.UseCases.Escolas.Command.Eliminar;
using Domain.Domain_Escolas.Models;
using Application.App_Escolas.UseCases.Escolas.Queries.ListarTodasEscola;
using Application.App_Escolas.UseCases.Escolas.Queries.ListarEscolaPeloId;

namespace Persistence.Repositories.Escolas
{
    [Route("api/escolas")]
    [ApiExplorerSettings(GroupName = "escolas")]
    [ApiController]
    public class EscolasController : ControllerBase
    {
        private readonly ISender _sender;
        public EscolasController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        public async Task<IActionResult> Criar(CriarEscolaCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> Editar(EditarEscolaCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _sender.Send( new EliminarEscolaCommand(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EscolasViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodasEscola()
        {
            var result = await _sender.Send(new ListarTodasEscolaQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EscolasViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodasEscola(int id)
        {
            var result = await _sender.Send(new ListarEscolaPeloIdQuery(id));
            return Ok(result);
        }

    }
}
