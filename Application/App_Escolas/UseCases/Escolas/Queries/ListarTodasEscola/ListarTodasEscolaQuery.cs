using Application.App_Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Models;

namespace Application.App_Escolas.UseCases.Escolas.Queries.ListarTodasEscola
{
    public  record ListarTodasEscolaQuery(): IQuery<List<EscolasViewModel>>;
}
