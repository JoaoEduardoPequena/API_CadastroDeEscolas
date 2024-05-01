using Application.App_Shared.Abstractions.Messaging;
namespace Application.App_Escolas.UseCases.Escolas.Command.Eliminar
{
       public record EliminarEscolaCommand(int id) : ICommand<object>;
}
