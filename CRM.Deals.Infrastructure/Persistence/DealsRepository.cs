using CRM.Deals.Domain;
using CRM.Deals.Domain.Models;
using CRM.Framework;
using Marten;

namespace CRM.Deals.Infrastructure.Persistence
{
    public sealed class DealsRepository(IDocumentSession session) : IDealsRepository
    {
        public async Task<Deal> LoadAsync(Guid id, CancellationToken ct)
        {
            var deal = await session.Events.AggregateStreamAsync<Deal>(id, token: ct);
            return deal ?? throw new Exception($"Cannot load deal with id: {id}"); 
        }

        public async Task SaveAsync(IEvent @event, CancellationToken ct)
        {
            session.Events.Append(@event.Id, @event);
            await session.SaveChangesAsync(ct);
        }
    }
}