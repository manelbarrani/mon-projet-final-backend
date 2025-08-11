using Application.Interfaces;
using Application.Setting;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Features.ProductFeature.Dtos;
using Microsoft.EntityFrameworkCore; // Pour InnerException détaillée si besoin

namespace Application.Features.ProductFeature.Commands
{
    public record AddProductCommandNew(
        string name,
        string description,
        decimal price,
        string category,
        string imageUrl,
        int stockQuantity,
        double? rating = null,
        bool? isNew = null
    ) : IRequest<ResponseHttp>;

    public class AddProductCommandNewHandler : IRequestHandler<AddProductCommandNew, ResponseHttp>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public AddProductCommandNewHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseHttp> Handle(AddProductCommandNew request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(request);

                product = await _productRepository.Post(product);
                await _productRepository.SaveChange(cancellationToken);

                return new ResponseHttp
                {
                    Resultat = _mapper.Map<ProductDTO>(product),
                    Status = StatusCodes.Status200OK
                };
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseHttp
                {
                    Fail_Messages = $"Erreur lors de l'enregistrement des modifications en base de données : {dbEx.InnerException?.Message ?? dbEx.Message}",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            catch (AutoMapperMappingException mapEx)
            {
                return new ResponseHttp
                {
                    Fail_Messages = $"Erreur de mapping AutoMapper : {mapEx.InnerException?.Message ?? mapEx.Message}",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception ex)
            {
                return new ResponseHttp
                {
                    Fail_Messages = $"Erreur inattendue : {ex.InnerException?.Message ?? ex.Message}",
                    Status = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
