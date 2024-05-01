using Application.App_Shared.Abstractions.Messaging;
using Domain.Domain_Escolas.Interfaces;

namespace Application.App_Escolas.UseCases.Escolas.Command.Eliminar
{
    public class EliminarEscolaCommandHandler : ICommandHandler<EliminarEscolaCommand, object>
    {
        private readonly IRepoEscolas _repoEscolas;
        public EliminarEscolaCommandHandler(IRepoEscolas repoEscolas)
        {
            _repoEscolas = repoEscolas;
        }
        public async Task<object> Handle(EliminarEscolaCommand request, CancellationToken cancellationToken)
        {
            string mensagem = string.Empty;
            var result = await _repoEscolas.EliminarEscola(request.id);
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
