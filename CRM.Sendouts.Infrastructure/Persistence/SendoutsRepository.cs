using CRM.Framework;
using CRM.Sendouts.Domain;
using CRM.Sendouts.Domain.Models;
using Marten;

namespace CRM.Sendouts.Infrastructure.Persistence
{
    public sealed class SendoutsRepository(IDocumentSession session) : ISendoutsRepository
    {
        public async Task<Sendout> LoadAsync(Guid id, CancellationToken ct)
        {
            var sendout = await session.Events.AggregateStreamAsync<Sendout>(id, token: ct);
            return sendout ?? throw new Exception($"Cannot load sendout with id: {id}"); 
        }

        public async Task SaveAsync(IEvent @event, CancellationToken ct)
        {
            session.Events.Append(@event.Id, @event);
            await session.SaveChangesAsync(ct);
        }
    }
}