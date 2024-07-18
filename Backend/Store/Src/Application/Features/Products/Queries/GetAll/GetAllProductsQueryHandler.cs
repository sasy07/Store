using MediatR;

namespace Application.Features.Product.Queries.GetAll;

public class GetAllProductsQueryHandler:IRequest<IEnumerable<Domain.Entities.Product>>
{
    
}