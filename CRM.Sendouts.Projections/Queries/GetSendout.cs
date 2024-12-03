using CRM.Sendouts.Projections.Dtos;
using Marten;
using MediatR;

namespace CRM.Sendouts.Projections.Queries
{
    public sealed record GetSendout(Guid Id) : IRequest<SendoutDto?>;
    
    public sealed record GetSendoutHandler : IRequestHandler<GetSendout, SendoutDto?>
    {
        private readonly IQuerySession _session;

        public GetSendoutHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<SendoutDto?> Handle(GetSendout request, CancellationToken cancellationToken)
        {
            return await _session.Events.AggregateStreamAsync<SendoutDto>(request.Id, token: cancellationToken);
        }
    }
}