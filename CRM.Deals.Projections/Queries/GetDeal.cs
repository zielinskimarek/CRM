using CRM.Deals.Projections.Dtos;
using CRM.Framework;
using Marten;

namespace CRM.Deals.Projections.Queries
{
    public sealed record GetDeal(Guid Id) : IQuery<DealDto?>;
    
    public sealed class GetDealHandler(IQuerySession session) : IQueryHandler<GetDeal, DealDto?>
    {
        public async Task<DealDto?> Handle(GetDeal request, CancellationToken cancellationToken)
        {
            return await session.Events.AggregateStreamAsync<DealDto>(request.Id, token: cancellationToken);
        }
    }
}