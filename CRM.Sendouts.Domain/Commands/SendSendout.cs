using CRM.Framework;
using CRM.Sendouts.Domain.Aggregate;

namespace CRM.Sendouts.Domain.Commands
{
    public sealed record SendSendout(Guid Id) : ICommand
    {
        public DateTime SentAt { get; } = DateTime.UtcNow;
    }
}