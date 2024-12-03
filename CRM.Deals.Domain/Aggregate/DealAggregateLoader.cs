using CRM.Deals.Domain.Commands;
using CRM.Deals.Domain.Models;
using CRM.Framework;
using MediatR;

namespace CRM.Deals.Domain.Aggregate
{
    public sealed class DealAggregateLoader<TRequest, TResponse>(
        AggregateContext<Deal> aggregateContext,
        IDealsRepository repository)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
        where TResponse : IEvent
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Deal deal;
            if (request is CreateDeal createDeal)
            {
                deal = new Deal(request.Id, 
                    createDeal.CreatedAt, createDeal.CreatedBy, createDeal.OrganizationId, DealStatus.Created);
            }
            else
            {
                var aggregateRoot = await repository.LoadAsync(request.Id, cancellationToken);
                deal = (aggregateRoot as Deal)!;
            }

            aggregateContext.SetAggregateRoot(deal);
            return await next();
        }
    }
}