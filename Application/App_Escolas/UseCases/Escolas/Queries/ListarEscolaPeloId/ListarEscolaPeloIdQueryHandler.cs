using Application.Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Interfaces;
using Domain.Domain_Escolas.Models;

namespace Application.App_Escolas.UseCases.Escolas.Queries.ListarEscolaPeloId
{
    public class ListarEscolaPeloIdQueryHandler:IQueryHandler<ListarEscolaPeloIdQuery,EscolasViewModel>
    {
        private readonly IRepoEscolas _repoEscolas;
        public ListarEscolaPeloIdQueryHandler(IRepoEscolas repoEscolas)
        {
            _repoEscolas = repoEscolas;
        }
        public async Task<EscolasViewModel> Handle(ListarEscolaPeloIdQuery request, CancellationToken cancellationToken)
        {
            var objEscola = await _repoEscolas.ListarEscolaPeloId(request.id);
            return objEscola;
        }
    }
}
