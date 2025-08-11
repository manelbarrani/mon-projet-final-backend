using Application.Features.ProductFeature.Commands;
using Application.Features.ProductFeature.Queries;
using Application.Features.ProductFeature.Validators;
using Application.Setting;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Product")]
    public class ProductControllerNew : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductControllerNew(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Ajouter un produit", Tags = new[] { "Product" })]
        public async Task<ActionResult> Add(AddProductCommandNew cmd)
        {
            try
            {
                ResponseHttp addProductResult;
                AddProductCommandNewValidator validator = new();
                addProductResult = validator.Validate(new ValidationContext<AddProductCommandNew>(cmd));

                if (addProductResult.Status == StatusCodes.Status400BadRequest)
                    return BadRequest(addProductResult);

                addProductResult = await _mediator.Send(cmd);
                return Ok(addProductResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("")]
        [SwaggerOperation(Summary = "Modifier un produit", Tags = new[] { "Product" })]
        public async Task<ActionResult> Update([FromBody] UpdateProductCommandNew cmd)
        {
            try
            {
                ResponseHttp updateProductResult;
                UpdateProductCommandNewValidator validator = new();
                updateProductResult = validator.Validate(new ValidationContext<UpdateProductCommandNew>(cmd));

                if (updateProductResult.Status == StatusCodes.Status400BadRequest)
                    return BadRequest(updateProductResult);

                updateProductResult = await _mediator.Send(cmd);
                return Ok(updateProductResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer un produit", Tags = new[] { "Product" })]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommandNew(id));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtenir un produit par ID", Tags = new[] { "Product" })]
        public async Task<ActionResult> Get(Guid id)
        {
            GetProductByIdNewQuery qr = new(id);
            var result = await _mediator.Send(qr);
            return Ok(result);
        }

        [HttpGet("")]
        [SwaggerOperation(Summary = "Lister tous les produits", Tags = new[] { "Product" })]
        public async Task<ActionResult> Get(int? pageNumber, int? pageSize)
        {
            var result = await _mediator.Send(new GetAllProductNewQuery(pageNumber, pageSize));
            return Ok(result);
        }
    }
}
