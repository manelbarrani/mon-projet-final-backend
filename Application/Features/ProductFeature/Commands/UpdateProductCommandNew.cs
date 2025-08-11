using Application.Features.ProductFeature.Dtos;
using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ProductFeature.Commands
{
    public record UpdateProductCommandNew(
       Guid ProductId,
       string? Name = null,
       string? Category = null,
       string? Description = null,
       decimal Price = 0,
       string? ImageUrl = null,
       int StockQuantity = 0,
       double? Rating = null,
       bool? IsNew = null
    ) : IRequest<ResponseHttp>
    {
        public class UpdateProductCommandNewHandler : IRequestHandler<UpdateProductCommandNew, ResponseHttp>
        {
            private readonly IProductRepository ProductRepository;
            private readonly IMapper _mapper;

            public UpdateProductCommandNewHandler(IProductRepository ProductRepository, IMapper mapper)
            {
                this.ProductRepository = ProductRepository;
                _mapper = mapper;
            }

            public async Task<ResponseHttp> Handle(UpdateProductCommandNew request, CancellationToken cancellationToken)
            {
                Product? Product = await ProductRepository.GetById(request.ProductId);

                if (Product == null)
                {
                    return new ResponseHttp
                    {
                        Resultat = null,
                        Fail_Messages = "Product with this Id not found.",
                        Status = StatusCodes.Status400BadRequest
                    };
                }

                if (request.Name != null) Product.Name = request.Name;
                if (request.Category != null) Product.Category = request.Category;
                if (request.Description != null) Product.Description = request.Description;
                if (request.Price != 0) Product.Price = request.Price;
                if (request.ImageUrl != null) Product.ImageUrl = request.ImageUrl;
                if (request.StockQuantity != 0) Product.StockQuantity = request.StockQuantity;
                if (request.Rating != null) Product.Rating = request.Rating;
                if (request.IsNew != null) Product.IsNew = request.IsNew;

                await ProductRepository.Update(Product);
                await ProductRepository.SaveChange(cancellationToken);

                var ProductToReturn = _mapper.Map<ProductDTO>(Product);

                return new ResponseHttp
                {
                    Resultat = ProductToReturn,
                    Status = StatusCodes.Status200OK
                };
            }
        }
    }
}
