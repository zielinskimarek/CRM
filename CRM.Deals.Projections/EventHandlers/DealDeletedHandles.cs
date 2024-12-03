using CRM.Deals.Domain.Events;
using CRM.Framework;

namespace CRM.Deals.Projections.EventHandlers
{
    public sealed class DealDeletedHandles : IHandles<DealDeleted>
    {
        public Task Handle(DealDeleted @event, CancellationToken cancellationToken)
        {
            // handle event
            return Task.CompletedTask;
        }
    }
}