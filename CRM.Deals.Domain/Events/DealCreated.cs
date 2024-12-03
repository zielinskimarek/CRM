using CRM.Framework;

namespace CRM.Deals.Domain.Events
{
    public sealed record DealCreated(Guid Id, DateTime CreatedAt, string CreatedBy, long OrganizationId) : IEvent;
}