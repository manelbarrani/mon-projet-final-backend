using Application.Features.NavigatorFeature.Commands;
using Application.Features.NavigatorFeature.Queries;
using Application.Features.VehiculeFeature.Commands;
using Application.Features.VehiculeFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VehiculeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> AddVehicule(AddVehiculeCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetVehiculeById(Guid id)
        {
            GetVehiculeByIdQuery qr = new(id);
            var result = await _mediator.Send(qr);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetListVehicules()
        {
            var result = await _mediator.Send(new GetListVehiculeQuery());

            return Ok(result);
        }
    }
}
