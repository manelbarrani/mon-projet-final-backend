using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ProductFeature.Commands
{
    public record DeleteProductCommandNew(
         Guid ProductId
        )
        : IRequest<ResponseHttp>
    {
        public class DeleteProductCommandNewHandler : IRequestHandler<DeleteProductCommandNew, ResponseHttp>
        {
            private readonly IProductRepository productRepository;

            public DeleteProductCommandNewHandler(IProductRepository productRepository)
            {
                this.productRepository = productRepository;
            }

            public async Task<ResponseHttp> Handle(DeleteProductCommandNew request, CancellationToken cancellationToken)
            {
                var product = await productRepository.GetById(request.ProductId);

                if (product == null)
                {
                    return new ResponseHttp
                    {
                        Fail_Messages = "No product found",
                        Status = StatusCodes.Status400BadRequest,
                    };
                }

                await productRepository.SoftDelete(request.ProductId);
                await productRepository.SaveChange(cancellationToken);

                return new ResponseHttp
                {
                    Status = StatusCodes.Status200OK,
                };
            }
        }
    }
}

