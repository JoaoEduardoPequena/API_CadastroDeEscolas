using Application.App_Shared.Abstractions.Messaging;

namespace Application.App_Escolas.UseCases.Escolas.Command.Criar
{
    public record  CriarEscolaCommand
    (
       string nome,
       string email,
       int numerosalas,
       string provincia
    ):ICommand<object>;

}
