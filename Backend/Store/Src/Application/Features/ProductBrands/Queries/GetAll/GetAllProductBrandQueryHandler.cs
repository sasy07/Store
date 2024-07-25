using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductBrands.Queries.GetAll;

public class GetAllProductBrandQueryHandler : IRequestHandler<GetAllProductBrandQuery,IEnumerable<ProductBrand>>
{
    private readonly IUnitOfWork _uow;

    public GetAllProductBrandQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<ProductBrand>> Handle(GetAllProductBrandQuery request
        , CancellationToken cancellationToken)
        => await _uow.Repository<ProductBrand>().GetAllAsync(cancellationToken);
}