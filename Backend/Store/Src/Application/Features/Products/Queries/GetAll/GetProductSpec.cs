using System.Linq.Expressions;
using Application.Contracts.Specification;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetAll;

public class GetProductSpec : BaseSpecification<Product>
{
    public GetProductSpec(GetAllProductsQuery specParams)
        :base(Expression.ExpressionSpec(specParams))
    {
        AddInclude(x=>x.ProductBrand);
        AddInclude(x=>x.ProductType);

        if (specParams.TypeSort == TypeSort.Desc)
        {
            switch (specParams.Sort)
            {
                case 1:
                    AddOrderByDesc(x=>x.Title);
                    break;
                case 2:
                    AddOrderByDesc(x=>x.ProductType.Title);
                    break;
                default:
                    AddOrderByDesc(x=>x.Title);
                    break;
            }
        }
        else
        {
            switch (specParams.Sort)
            {
                case 1:
                    AddOrderBy(x=>x.Title);
                    break;
                case 2:
                    AddOrderBy(x=>x.ProductType.Title);
                    break;
                default:
                    AddOrderBy(x=>x.Title);
                    break;
            }
        }
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1) , specParams.PageSize , true);
    }

  

    public GetProductSpec(int id) : base (x=>x.Id == id)
    {
        AddInclude(x=>x.ProductBrand);
        AddInclude(x=>x.ProductType);
    }
  
}

public class ProductsCountSpec : BaseSpecification<Product>
{
    public ProductsCountSpec(GetAllProductsQuery specParams) : base(Expression.ExpressionSpec(specParams))
    {
        IsPagingEnabled = false;
    }
}

public static class Expression
{
    public static Expression<Func<Product, bool>> ExpressionSpec(GetAllProductsQuery specParams)
    {
        return x=>(!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId)
                  && (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
                  && (string.IsNullOrWhiteSpace(specParams.Search) || x.Title.ToLower().Contains(specParams.Search));
    }
}