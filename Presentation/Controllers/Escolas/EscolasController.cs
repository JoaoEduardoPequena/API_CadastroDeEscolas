using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.App_Escolas.UseCases.Escolas.Command.Criar;
using Application.App_Escolas.UseCases.Escolas.Command.Editar;
using Application.App_Escolas.UseCases.Escolas.Command.Eliminar;
using Domain.Domain_Escolas.Models;
using Application.App_Escolas.UseCases.Escolas.Queries.ListarTodasEscola;
using Application.App_Escolas.UseCases.Escolas.Queries.ListarEscolaPeloId;
using Microsoft.AspNetCore.Http.Internal;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Azure.Core;
using DocumentFormat.OpenXml.Office2016.Excel;

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

                        var tasks = new List<Task>();

                        for (int row = 2; row <= rowCount; row++) // Começa da linha 2 para ignorar cabeçalhos
                        {
                            tasks.Add(ProcessRowAsync(worksheet, row));
                        }

                        await Task.WhenAll(tasks);
                    }
                }

                return Ok("Dados do Excel importados com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao importar dados do Excel: {ex.Message}");
            }
        }


        private async Task ProcessRowAsync(IXLWorksheet worksheet, int row)
        {

            try
            {
                var nome = worksheet.Cell(row, 1).Value.ToString();
                var email = worksheet.Cell(row, 2).Value.ToString();
                var numeroSalas = worksheet.Cell(row, 3).Value.ToString();
                var provincia = worksheet.Cell(row, 4).Value.ToString();
                //var entity = new YourEntity
                //{
                //    nome = worksheet.Cell(row, 1).Value.ToString(),
                //    Column2 = worksheet.Cell(row, 2).Value.ToString(),
                //    // Map other columns as needed
                //};

                //_context.YourEntities.Add(entity);

                var result2 = await _sender.Send(new CriarEscolaCommand(nome, email, int.Parse(numeroSalas), provincia));

                if (row % 1000 == 0) // Batch save changes to the database every 1000 rows
                {
                    //var request = new CriarEscolaCommand();
                    // Salvar no Banco De daos aqui
                    var result = await _sender.Send(new CriarEscolaCommand(nome,email,int.Parse(numeroSalas),provincia));
                }
            }
            catch (Exception ex)
            {
                // Handle or log any exceptions
            }
        }
    }


}
