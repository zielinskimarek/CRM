using CRM.Framework;

namespace CRM.Sendouts.Domain.Events
{
    public sealed record SendoutScheduled(
        Guid Id,
        DateTime ScheduledAt,
        IEnumerable<string> Recipients) : IEvent;
}