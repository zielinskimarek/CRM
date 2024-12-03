using MediatR;

namespace CRM.Framework
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}