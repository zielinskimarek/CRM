using MediatR;

namespace CRM.Framework
{
    public interface IConsumes<in TCommand> : IRequestHandler<TCommand, IEvent> where TCommand : ICommand
    {
    }
}