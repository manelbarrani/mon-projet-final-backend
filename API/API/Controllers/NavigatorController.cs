using Application.Features.NavigatorFeature.Commands;
using Application.Features.NavigatorFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigatorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NavigatorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> AddNavigator(AddNavigatorCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetNavigatorById(Guid id)
        {
            GetNavigatorByIdQuery qr = new(id);
            var result = await _mediator.Send(qr);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetListNavigators()
        {
            var result = await _mediator.Send(new GetListNavigatorQuery());

            return Ok(result);
        }
    }
}
