using Application.App_Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Entities;
using Domain.Domain_Escolas.Interfaces;
using Mapster;

namespace Application.App_Escolas.UseCases.Escolas.Command.Editar
{
    public class EditarEscolaCommandHandler : ICommandHandler<EditarEscolaCommand, object>
    {
        private readonly IRepoEscolas _repoEscolas;
        public EditarEscolaCommandHandler(IRepoEscolas repoEscolas)
        {
            _repoEscolas = repoEscolas;
        }
        public async Task<object> Handle(EditarEscolaCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Adapt<TbEscolas>();
            string mensagem = string.Empty;
            var result = await _repoEscolas.EditarEscola(dto, request.id);
            if (result >= 1)
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
