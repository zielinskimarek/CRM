using MediatR;

namespace CRM.Framework
{
    public interface ICommand : IMessage, IRequest<IEvent>
    {
    }
}