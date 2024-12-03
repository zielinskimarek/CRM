using CRM.Framework;

namespace CRM.Sendouts.Domain.Commands
{
    public sealed record AddRecipient(Guid Id, string Email) : ICommand
    {
        public DateTime AddedAt { get; } = DateTime.UtcNow;
    }
}