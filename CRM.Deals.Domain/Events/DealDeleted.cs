using CRM.Framework;

namespace CRM.Deals.Domain.Events
{
    public sealed record DealDeleted(Guid Id) : IEvent;
}