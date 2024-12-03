using CRM.Deals.Domain.Events;
using CRM.Framework;

namespace CRM.Deals.Domain.Models
{
    public sealed class Deal : IEntity
    {
        public Deal() { }
        
        public Deal(Guid id, DateTime createdAt, string createdBy, long organizationId, DealStatus status)
        {
            Id = id;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            OrganizationId = organizationId;
            Status = status;
        }
        
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }
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
            // TODO implement
        }
    }
}