using CRM.Framework;
using CRM.Sendouts.Domain.Commands;
using CRM.Sendouts.Domain.Events;
using CRM.Sendouts.Domain.Exceptions;
using CRM.Sendouts.Domain.Models;

namespace CRM.Sendouts.Domain.Aggregate
{
    public sealed class SendoutAggregate(AggregateContext<Sendout> aggregateContext) :
        IConsumes<CreateSendout>,
        IConsumes<DesignSendout>,
        IConsumes<AddRecipient>,
        IConsumes<SendSendout>
    {
        private readonly Sendout _sendout = aggregateContext.AggregateRoot ?? throw new ArgumentNullException(nameof(aggregateContext.AggregateRoot));

        public Task<IEvent> Handle(CreateSendout command, CancellationToken cancellationToken)
        {
            if (_sendout.Status != SendoutStatus.None)
            {
                throw new InvalidSendoutStatusException(
                    $"Cannot change sendout status from `{_sendout.Status.ToString()}` to `Created`.", command.Id);
            }

            var @event = new SendoutCreated(command.Id, command.CreatedAt, command.Name);
            return Task.FromResult<IEvent>(@event);
        }
        
        public Task<IEvent> Handle(DesignSendout command, CancellationToken cancellationToken)
        {
            if (_sendout.Status != SendoutStatus.Created)
            {
                throw new InvalidSendoutStatusException(
                    $"Cannot change sendout status from `{_sendout.Status.ToString()}` to `Designed`.", command.Id);
            }
            
            if (_sendout.ModifiedAt > command.DesignedAt)
            {
                throw new InvalidSendoutModificationDateException(
                    $"Designed date `{command.DesignedAt} is before the current modified date: {_sendout.ModifiedAt}.",
                    command.Id);
            }

            return Task.FromResult<IEvent>(new SendoutDesigned(command.Id, command.DesignedAt));
        }
        
        public Task<IEvent> Handle(AddRecipient command, CancellationToken cancellationToken)
        {
            if (_sendout.Status is SendoutStatus.None or SendoutStatus.Sent)
            {
                throw new InvalidSendoutStatusException(
                    $"Cannot add recipient to sendout with status: `{_sendout.Status.ToString()}`.", command.Id);
            }
            
            return Task.FromResult<IEvent>(new RecipientAdded(command.Id, command.Email, command.AddedAt));
        }
        
        public Task<IEvent> Handle(SendSendout command, CancellationToken cancellationToken)
        {
            if (_sendout.Status != SendoutStatus.Designed)
            {
                throw new InvalidSendoutStatusException(
                    $"Cannot change sendout status from `{_sendout.Status.ToString()}` to `Sent`.", command.Id);
            }
            
            if (_sendout.ModifiedAt > command.SentAt)
            {
                throw new InvalidSendoutModificationDateException(
                    $"Designed date `{command.SentAt} is before the current modified date: {_sendout.ModifiedAt}.",
                    command.Id);
            }

            if (!_sendout.Recipients.Any())
            {
                throw new NoRecipientsException("Cannot send a sendout with no recipients", command.Id);
            }

            return Task.FromResult<IEvent>(new SendoutScheduled(command.Id, command.SentAt, _sendout.Recipients));
        }
    }
}