using Application.Contracts;
using Application.Dtos.Products;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.GetAll;

public class GetAllProductsQuery : RequestParametersBasic, IRequest<PaginationResponse<ProductDto>>, ICacheQuery
{
    public int PageId { get; set; }

    public int? BrandId { get; set; }
    public int? TypeId { get; set; }

    //TODO : don't display this in parameters 
    public int HoursSaveData => 1;
}