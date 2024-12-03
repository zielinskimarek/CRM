using MediatR;

namespace CRM.Framework
{
    public interface IEvent : IMessage, INotification
    {
    }
}