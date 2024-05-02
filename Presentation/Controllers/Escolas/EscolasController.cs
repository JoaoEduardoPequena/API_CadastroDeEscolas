using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.App_Escolas.UseCases.Escolas.Command.Criar;
using Application.App_Escolas.UseCases.Escolas.Command.Editar;
using Application.App_Escolas.UseCases.Escolas.Command.Eliminar;
using Domain.Domain_Escolas.Models;
using Application.App_Escolas.UseCases.Escolas.Queries.ListarTodasEscola;
using Application.App_Escolas.UseCases.Escolas.Queries.ListarEscolaPeloId;
using ClosedXML.Excel;

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
            var result = await _sender.Send(new EliminarEscolaCommand(id));
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

        [HttpPost("uploadFileExcel")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        public async Task<IActionResult> UploadFileExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Arquivo inválido.");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.First(); // Assumindo que os dados estejam na primeira planilha
                        var rowCount = worksheet.RowsUsed().Count();

                        for (int row = 2; row <= rowCount; row++) // Começa da linha 2 para ignorar cabeçalhos
                        {
                            var nome = worksheet.Cell(row, 1).Value.ToString();
                            var email = worksheet.Cell(row, 2).Value.ToString();
                            var numeroSalas = worksheet.Cell(row, 3).Value.ToString();
                            var provincia = worksheet.Cell(row, 4).Value.ToString();
                            await _sender.Send(new CriarEscolaCommand(nome, email, int.Parse(numeroSalas), provincia)); // Inserir as informações para base de dados

                        }

                    }
                }

                return Ok("Dados do Excel importados com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao importar dados do Excel: {ex.Message}");
            }
        }
    }


}
