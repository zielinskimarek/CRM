using CRM.Framework;
using CRM.Sendouts.Domain.Aggregate;

namespace CRM.Sendouts.Domain.Commands
{
    public sealed record DesignSendout(Guid Id) : ICommand
    {
        public DateTime DesignedAt { get; } = DateTime.UtcNow;
    }
}