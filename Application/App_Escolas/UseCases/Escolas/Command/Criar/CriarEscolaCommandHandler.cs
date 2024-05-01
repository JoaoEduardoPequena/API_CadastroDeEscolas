using Application.App_Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Entities;
using Domain.Domain_Escolas.Interfaces;
using Mapster;

namespace Application.App_Escolas.UseCases.Escolas.Command.Criar
{
    public class CriarEscolaCommandHandler : ICommandHandler<CriarEscolaCommand, object>
    {
        private readonly IRepoEscolas _repoEscolas;
        public CriarEscolaCommandHandler(IRepoEscolas repoEscolas)
        {
            _repoEscolas = repoEscolas;
        }

        public async Task<object> Handle(CriarEscolaCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Adapt<TbEscolas>();
            string mensagem=string.Empty;
            var result = await _repoEscolas.CriarEscola(dto);
            if (result >=1)
            {
                mensagem = "Operação realizada com sucesso";
                return mensagem;
            }
            else
            {
                mensagem = "Ocorreu um erro tentar realizar esta operação";
                return mensagem;
            }
        }
    }
}
