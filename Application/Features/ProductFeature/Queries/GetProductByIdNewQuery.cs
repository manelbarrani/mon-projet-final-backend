using Application.Features.ProductFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ProductFeature.Queries
{
    public record GetProductByIdNewQuery(
        Guid ProductId
    ) : IRequest<ResponseHttp>
    {
        public class GetProductByIdNewQueryHandler : IRequestHandler<GetProductByIdNewQuery, ResponseHttp>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper _mapper;

            public GetProductByIdNewQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                this.productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(GetProductByIdNewQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

                    if (product == null)
                    {
                        return new ResponseHttp
                        {
                            Status = StatusCodes.Status404NotFound,
                            Fail_Messages = "Product not found!"
                        };
                    }

                    return new ResponseHttp
                    {
                        Resultat = _mapper.Map<ProductDTO>(product),
                        Status = StatusCodes.Status200OK
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseHttp
                    {
                        Fail_Messages = ex.Message,
                        Status = StatusCodes.Status400BadRequest
                    };
                }
            }
        }
    }
}
