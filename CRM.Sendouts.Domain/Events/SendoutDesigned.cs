using CRM.Framework;
using CRM.Sendouts.Domain.Aggregate;

namespace CRM.Sendouts.Domain.Events
{
    public sealed record SendoutDesigned(Guid Id, DateTime DesignedAt) : IEvent;
}