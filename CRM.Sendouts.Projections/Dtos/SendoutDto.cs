using CRM.Sendouts.Domain.Events;
using CRM.Sendouts.Domain.Models;

namespace CRM.Sendouts.Projections.Dtos
{
    public sealed record SendoutDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = null!;
        public DateTime ModifiedAt { get; set; }
        public SendoutStatus Status { get; set; }
        public IList<SendoutRecipient> Recipients { get; } = new List<SendoutRecipient>();

        public void Apply(SendoutCreated @event)
        {
            Name = @event.Name;
            ModifiedAt = @event.CreatedAt;
            Status = SendoutStatus.Created;
        }
        
        public void Apply(SendoutDesigned @event)
        {
            ModifiedAt = @event.DesignedAt;
            Status = SendoutStatus.Designed;
        }
        
        public void Apply(RecipientAdded @event)
        {
            Recipients.Add(new SendoutRecipient(@event.Email));
        }

        public void Apply(SendoutScheduled @event)
        {
            ModifiedAt = @event.ScheduledAt;
            Status = SendoutStatus.Sent;
        }
    }

    public sealed record SendoutRecipient(string Email);
}