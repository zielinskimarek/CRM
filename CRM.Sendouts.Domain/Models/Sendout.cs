using CRM.Framework;
using CRM.Sendouts.Domain.Events;

namespace CRM.Sendouts.Domain.Models
{
    public sealed class Sendout : IEntity
    {
        public Sendout() { }
        
        public Guid Id { get; set; }
        public string Name { get; private set; } = null!;
        public SendoutStatus Status { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public IList<string> Recipients { get; } = new List<string>();
        
        public void Apply(SendoutCreated @event)
        {
            Name = @event.Name;
            Status = SendoutStatus.Created;
            ModifiedAt = @event.CreatedAt;
        }
        
        public void Apply(SendoutDesigned @event)
        {
            Status = SendoutStatus.Designed;
            ModifiedAt = @event.DesignedAt;
        }
        
        public void Apply(RecipientAdded @event)
        {
            ModifiedAt = @event.AddedAt;
            Recipients.Add(@event.Email);
        }

        public void Apply(SendoutScheduled @event)
        {
            Status = SendoutStatus.Sent;
            ModifiedAt = @event.ScheduledAt;
        }
    }
}