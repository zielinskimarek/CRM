using CRM.Framework;

namespace CRM.Sendouts.Domain.Events
{
    public sealed record RecipientAdded(Guid Id, string Email, DateTime AddedAt) : IEvent;
}