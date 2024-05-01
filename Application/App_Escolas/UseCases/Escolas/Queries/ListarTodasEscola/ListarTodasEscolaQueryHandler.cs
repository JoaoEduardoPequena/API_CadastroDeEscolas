using Application.Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Interfaces;
using Domain.Domain_Escolas.Models;

namespace Application.App_Escolas.UseCases.Escolas.Queries.ListarTodasEscola
{
    public class ListarTodasEscolaQueryHandler : IQueryHandler<ListarTodasEscolaQuery, List<EscolasViewModel>>
    {
        private readonly IRepoEscolas _repoEscolas;
        public ListarTodasEscolaQueryHandler(IRepoEscolas repoEscolas)
        {
            _repoEscolas = repoEscolas;
        }
        public async Task<List<EscolasViewModel>> Handle(ListarTodasEscolaQuery request, CancellationToken cancellationToken)
        {
            var result = await _repoEscolas.ListarTodasEscola();
            return result;
        }
    }
}
