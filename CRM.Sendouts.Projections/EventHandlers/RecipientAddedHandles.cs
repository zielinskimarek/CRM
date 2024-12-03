using CRM.Framework;
using CRM.Sendouts.Domain.Events;

namespace CRM.Sendouts.Projections.EventHandlers
{
    public sealed class RecipientAddedHandles : IHandles<RecipientAdded>
    {
        public Task Handle(RecipientAdded @event, CancellationToken cancellationToken)
        {
            // handle event
            return Task.CompletedTask;
        }
    }
}