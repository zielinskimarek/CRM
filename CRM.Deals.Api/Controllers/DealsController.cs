using CRM.Deals.Domain.Commands;
using CRM.Deals.Domain.Models;
using CRM.Deals.Projections.Queries;
using CRM.Framework;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Deals.Api.Controllers
{
    [Route("deals")]
    [ApiController]
    public sealed class DealsController(ICommandsQueue commandsQueue, IMediator mediator) : Controller
    {
        [HttpGet, Route("{dealId}")]
        public async Task<IActionResult> Get(Guid dealId)
        {
            var result = await mediator.Send(new GetDeal(dealId));
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult Create(string createdBy, long organizationId)
        {
            var createDeal = new CreateDeal(createdBy, organizationId);
            commandsQueue.Queue(createDeal);
            return Ok(createDeal.Id);
        }

        [HttpPut, Route("{dealId}")]
        public IActionResult Update(Guid dealId, int? organizationId, DealStatus? status)
        {
            commandsQueue.Queue(new UpdateDeal(dealId, organizationId, status));
            return Ok();
        }

        [HttpDelete, Route("{dealId}")]
        public IActionResult Delete(Guid dealId)
        {
            commandsQueue.Queue(new DeleteDeal(dealId));
            return Ok();
        }
    }
}