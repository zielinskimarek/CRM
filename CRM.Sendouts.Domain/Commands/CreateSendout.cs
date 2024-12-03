using CRM.Framework;

namespace CRM.Sendouts.Domain.Commands
{
    public sealed record CreateSendout(string Name) : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}