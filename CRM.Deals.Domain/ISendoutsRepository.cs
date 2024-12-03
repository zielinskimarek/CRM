using CRM.Deals.Domain.Models;
using CRM.Framework;

namespace CRM.Deals.Domain;

public interface IDealsRepository
{
    Task<Deal> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(IEvent @event, CancellationToken ct);
}
