using Application.App_Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Models;

namespace Application.App_Escolas.UseCases.Escolas.Queries.ListarEscolaPeloId
{
    public  record ListarEscolaPeloIdQuery(int id):IQuery<EscolasViewModel>;
}
