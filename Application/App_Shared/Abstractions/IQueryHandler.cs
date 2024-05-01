using Application.App_Shared.Abstractions.Messaging;
using MediatR;

namespace Application.Shared.Abstractions.Messaging
{
    public interface IQueryHandler<in IQuery, TResponse> : IRequestHandler<IQuery, TResponse>
    where IQuery : IQuery<TResponse>
    {
    }
}



