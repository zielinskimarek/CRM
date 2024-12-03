using MediatR;

namespace CRM.Framework
{
    public interface IHandles<in TResponse> : INotificationHandler<TResponse> where TResponse : IEvent
    {
    }
}