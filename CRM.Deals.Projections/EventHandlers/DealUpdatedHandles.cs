using CRM.Deals.Domain.Events;
using CRM.Framework;

namespace CRM.Deals.Projections.EventHandlers
{
    public sealed class DealUpdatedHandles : IHandles<DealUpdated>
    {
        public Task Handle(DealUpdated @event, CancellationToken cancellationToken)
        {
            // handle event
            return Task.CompletedTask;
        }
    }
}