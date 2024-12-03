using CRM.Deals.Domain.Events;
using CRM.Framework;

namespace CRM.Deals.Projections.EventHandlers
{
    public sealed class DealCreatedHandles : IHandles<DealCreated>
    {
        public Task Handle(DealCreated @event, CancellationToken cancellationToken)
        {
            // handle event
            return Task.CompletedTask;
        }
    }
}