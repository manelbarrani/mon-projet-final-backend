using Application.Features.ProductFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ProductFeature.Queries
{
    public record GetAllProductNewQuery(int? pageNumber, int? pageSize) : IRequest<ResponseHttp>
    {
        public class GetAllProductNewQueryHandler : IRequestHandler<GetAllProductNewQuery, ResponseHttp>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper _mapper;

            public GetAllProductNewQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                this.productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(GetAllProductNewQuery request, CancellationToken cancellationToken)
            {
                var products = await productRepository.GetAllWithTypesAsync(request.pageNumber, request.pageSize, cancellationToken);

                if (products == null)
                    return new ResponseHttp
                    {
                        Fail_Messages = "No product found!",
                        Status = StatusCodes.Status400BadRequest,
                    };

                var productsToReturn = _mapper.Map<PagedList<ProductDTO>>(products);
                return new ResponseHttp
                {
                    Status = StatusCodes.Status200OK,
                    Resultat = productsToReturn
                };
            }
        }
    }
}
