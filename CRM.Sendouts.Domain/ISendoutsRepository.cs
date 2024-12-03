using CRM.Framework;
using CRM.Sendouts.Domain.Models;

namespace CRM.Sendouts.Domain;

public interface ISendoutsRepository
{
    Task<Sendout> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(IEvent @event, CancellationToken ct);
}
