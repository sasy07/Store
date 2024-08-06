using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Mapping.Resolvers;

public class ProductImageUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
{
    private readonly IConfiguration _configuration = configuration;

    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        => !string.IsNullOrWhiteSpace(source.PictureUrl)
            ? $"{_configuration["BackendUrl"]}{_configuration["LocationImages:ProductsImageLocation"]}{source.PictureUrl}"
            : null;
}