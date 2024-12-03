using CRM.Deals.Domain.Commands;
using CRM.Deals.Domain.Events;
using CRM.Deals.Domain.Models;
using CRM.Framework;

namespace CRM.Deals.Domain.Aggregate
{
    public sealed class DealAggregate(AggregateContext<Deal> aggregateContext) :
        IConsumes<CreateDeal>,
        IConsumes<UpdateDeal>,
        IConsumes<DeleteDeal>
    {
        private readonly Deal _deal = aggregateContext.AggregateRoot ?? throw new ArgumentNullException(nameof(aggregateContext.AggregateRoot));

        public Task<IEvent> Handle(CreateDeal command, CancellationToken cancellationToken)
        {
            var @event = new DealCreated(command.Id, command.CreatedAt, command.CreatedBy, command.OrganizationId);
            return Task.FromResult<IEvent>(@event);
        }

        public Task<IEvent> Handle(UpdateDeal command, CancellationToken cancellationToken)
        {
            var @event = new DealUpdated(command.Id, command.UpdatedAt, command.OrganizationId, command.Status);
            return Task.FromResult<IEvent>(@event);
        }

        public Task<IEvent> Handle(DeleteDeal request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}