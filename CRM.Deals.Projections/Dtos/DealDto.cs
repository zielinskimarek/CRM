using CRM.Deals.Domain.Events;
using CRM.Deals.Domain.Models;

namespace CRM.Deals.Projections.Dtos
{
    public sealed class DealDto
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public string CreatedBy { get; private set; } = null!;
        public long OrganizationId { get; private set; }
        public DealStatus Status { get; private set; }

        public void Apply(DealCreated @event)
        {
            Id = @event.Id;
            CreatedAt = @event.CreatedAt;
            CreatedBy = @event.CreatedBy;
            OrganizationId = @event.OrganizationId;
            Status = DealStatus.Created;
        }
        
        public void Apply(DealUpdated @event)
        {
            ModifiedAt = @event.UpdatedAt;
            if (@event.OrganizationId.HasValue)
            {
                OrganizationId = @event.OrganizationId!.Value;
            }
            
            if (@event.Status.HasValue)
            {
                Status = @event.Status!.Value;
            }
        }
        
        public void Apply(DealDeleted @event)
        {
            throw new NotImplementedException();
        }
    }
}