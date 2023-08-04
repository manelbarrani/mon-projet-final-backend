using Application.Features.ShipmentsFeature.Commands;
using Application.Features.ShipmentsFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShipmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> AddShipment(AddShipmentCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetShipmentById(Guid id)
        {
            GetShipmentByIdQuery qr = new(id);
            var result = await _mediator.Send(qr);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetShipmentDetailsById(Guid id)
        {
            GetShipmentDetailsQuery qr = new(id);
            var result = await _mediator.Send(qr);

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetListShipments()
        {
            var result = await _mediator.Send(new GetListShipmentQuery());

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetListShipmentsByStatus(int Status)
        {
            var result = await _mediator.Send(new GetListShipmentByStatusQuery(Status));

            return Ok(result);
        }
    }
}
