using System.Linq.Expressions;
using Application.Contracts.Specification;
using Domain.Entities.Base;

namespace Application.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
    Task<bool> AnyAsync(CancellationToken cancellationToken);
    Task<T> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAsyncSpec(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<int> CountAsyncSpec(ISpecification<T> spec, CancellationToken cancellationToken);
}