using Application.App_Shared.Abstractions.Messaging;
namespace Application.App_Escolas.UseCases.Escolas.Command.Editar
{
   public record EditarEscolaCommand
   (
     int id,
     string nome,
     string email,
     int numerosalas,
     string provincia
   ) : ICommand<object>;

}
