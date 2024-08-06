using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts;

public interface IUnitOfWork
{
    DbContext Context { get; }
    Task<int> Save(CancellationToken cancellationToken);
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
}