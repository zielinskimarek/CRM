using CRM.Framework;
using CRM.Sendouts.Domain.Commands;
using CRM.Sendouts.Projections.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Sendouts.Api.Controllers
{
    [Route("sendouts")]
    [ApiController]
    public class SendoutsController(ICommandsQueue commandsQueue, IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get(Guid sendoutId)
        {
            var result = await mediator.Send(new GetSendout(sendoutId));
            return result != null ? Ok(result) : NotFound();
        }
        
        [HttpPost]
        public IActionResult Create([FromQuery]string name)
        {
            var createSendout = new CreateSendout(name);
            commandsQueue.Queue(createSendout);
            return Ok(createSendout.Id);
        }

        [HttpPut, Route("{id}/send")]
        public IActionResult Send(Guid id)
        {
            var sendSendout = new SendSendout(id);
            commandsQueue.Queue(sendSendout);
            return Ok(sendSendout.Id);
        }

        [HttpPut, Route("{id}/design")]
        public IActionResult Design(Guid id)
        {
            commandsQueue.Queue(new DesignSendout(id));
            return Ok();
        }

        [HttpPut, Route("{id}/recipient")]
        public IActionResult AddRecipient(Guid id, [FromQuery] string email)
        {
            commandsQueue.Queue(new AddRecipient(id, email));
            return Ok();
        }
    }
}
