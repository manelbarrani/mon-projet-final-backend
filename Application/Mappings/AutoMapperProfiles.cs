using Application.Features.TestFeature.Commands;
using Application.Features.TestFeature.Dtos;
using Application.Features.ProductFeature.Commands;
using Application.Features.ProductFeature.Dtos;
using AutoMapper;
using Domain.Common;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // === Test Mappings ===
            CreateMap<AddTestCommandNew, Test>();
            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<PagedList<Test>, PagedList<TestDTO>>().ReverseMap();

            // === Product Mappings ===
            CreateMap<AddProductCommandNew, Product>();        // Note le 'product' minuscule
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<PagedList<Product>, PagedList<ProductDTO>>().ReverseMap();
        }
    }
}
