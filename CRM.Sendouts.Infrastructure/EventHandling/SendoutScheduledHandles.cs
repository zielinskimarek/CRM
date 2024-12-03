using CRM.Framework;
using CRM.Sendouts.Domain.Events;
using CRM.Sendouts.Infrastructure.Smtp;

namespace CRM.Sendouts.Infrastructure.EventHandling
{
    public sealed class SendoutScheduledHandles(ISmtpClient smtpClient) : IHandles<SendoutScheduled>
    {
        public async Task Handle(SendoutScheduled @event, CancellationToken cancellationToken)
        {
            foreach (var recipient in @event.Recipients)
            {
                await smtpClient.Send(recipient);
            }
        }
    }
}