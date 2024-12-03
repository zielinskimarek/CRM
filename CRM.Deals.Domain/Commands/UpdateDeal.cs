using CRM.Deals.Domain.Models;
using CRM.Framework;

namespace CRM.Deals.Domain.Commands
{
    public sealed record UpdateDeal(Guid Id, int? OrganizationId, DealStatus? Status) : ICommand
    {
        public DateTime UpdatedAt { get; } = DateTime.UtcNow;
    }
}