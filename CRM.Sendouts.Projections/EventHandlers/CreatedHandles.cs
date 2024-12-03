using CRM.Framework;
using CRM.Sendouts.Domain.Events;

namespace CRM.Sendouts.Projections.EventHandlers
{
    public sealed class CreatedHandles : IHandles<SendoutCreated>
    {
        public Task Handle(SendoutCreated @event, CancellationToken cancellationToken)
        {
            // handle event
            return Task.CompletedTask;
        }
    }
}