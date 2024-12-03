using CRM.Framework;
using CRM.Sendouts.Domain.Commands;
using CRM.Sendouts.Domain.Models;
using MediatR;

namespace CRM.Sendouts.Domain.Aggregate
{
    public sealed class SendoutAggregateLoader<TRequest, TResponse>(
        AggregateContext<Sendout> aggregateContext,
        ISendoutsRepository repository)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
        where TResponse : IEvent
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Sendout sendout;
            if (request is CreateSendout)
            {
                sendout = new Sendout
                {
                    Id = request.Id
                };
            }
            else
            {
                var aggregateRoot = await repository.LoadAsync(request.Id, cancellationToken);
                sendout = aggregateRoot;
            }

            aggregateContext.SetAggregateRoot(sendout);
            return await next();
        }
    }
}