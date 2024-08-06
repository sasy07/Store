using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Application.Contracts.Specification;

public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Pridicate { get; }
    public List<Expression<Func<T, object>>> Include { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDesc { get; private set; }
    public int Take { get; set; }
    public int Skip { get; set;}
    public bool IsPagingEnabled { get; set;} = true;

    public BaseSpecification()
    {
        
    }

    public BaseSpecification(Expression<Func<T , bool>> pridicate)
    {
        Pridicate = pridicate;
    }

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Include.Add(include);
    }
    protected void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }

    protected void ApplyPaging(int skip, int take , bool isPagingEnabled = true)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = isPagingEnabled;
    }
    
    
}