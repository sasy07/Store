using Application.Contracts;
using Application.Dtos.Products;
using Application.Features.Products.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Products.Queries.Get;

public class GetProductQueryHandler(IUnitOfWork uow, IMapper mapper) : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IMapper _mapper = mapper;

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpec(request.Id);
        var entity =  await _uow.Repository<Product>().GetEntityWithSpec(spec, cancellationToken);
        if (entity == null) throw new NotFoundEntityException();
        return _mapper.Map<ProductDto>(entity);
    }
}