using Application.Contracts;
using Application.Dtos.Products;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries.GetAll;

public class GetAllProductsQueryHandler(IUnitOfWork uow , IMapper mapper) : IRequestHandler<GetAllProductsQuery, PaginationResponse<ProductDto>>
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResponse<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpec(request);
        var count = await _uow.Repository<Product>().CountAsyncSpec(new ProductsCountSpec(request), cancellationToken);
        var entity =  await _uow.Repository<Product>().ListAsyncSpec(spec, cancellationToken);
        var model = _mapper.Map<IEnumerable<ProductDto>>(entity);
        return new PaginationResponse<ProductDto>(request.PageIndex, request.PageSize, count, model);
    } 
}