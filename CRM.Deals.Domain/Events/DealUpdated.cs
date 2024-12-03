using CRM.Deals.Domain.Models;
using CRM.Framework;

namespace CRM.Deals.Domain.Events
{
    public sealed record DealUpdated(Guid Id, DateTime UpdatedAt, int? OrganizationId, DealStatus? Status) : IEvent;
}