using CRM.Framework;
using CRM.Sendouts.Domain.Events;

namespace CRM.Sendouts.Projections.EventHandlers
{
    public sealed class DesignedHandles : IHandles<SendoutDesigned>
    {
        public Task Handle(SendoutDesigned @event, CancellationToken cancellationToken)
        {
            // handle event
            return Task.CompletedTask;
        }
    }
}