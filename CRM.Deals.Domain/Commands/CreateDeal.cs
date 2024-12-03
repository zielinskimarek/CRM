using CRM.Framework;

namespace CRM.Deals.Domain.Commands
{
    public sealed record CreateDeal(string CreatedBy, long OrganizationId) : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}