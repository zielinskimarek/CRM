using CRM.Framework;

namespace CRM.Sendouts.Domain.Events
{
    public sealed record SendoutCreated(Guid Id, DateTime CreatedAt, string Name) : IEvent;
}