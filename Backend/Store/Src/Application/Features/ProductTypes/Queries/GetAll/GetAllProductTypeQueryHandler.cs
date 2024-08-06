using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductTypes.Queries.GetAll;

public class GetAllProductTypeQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllProductTypeQuery, IEnumerable<ProductType>>
{
    public async Task<IEnumerable<ProductType>> Handle(GetAllProductTypeQuery request
        , CancellationToken cancellationToken)
    {
        var spec = new GetProductTypeSpec();
        return await uow.Repository<ProductType>().ListAsyncSpec(spec, cancellationToken);
    }
}