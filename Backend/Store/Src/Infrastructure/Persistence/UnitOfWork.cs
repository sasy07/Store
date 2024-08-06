using Application.Contracts;
using Domain.Entities.Base;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public DbContext Context => context;

    public async Task<int> Save(CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
        => new GenericRepository<T>(context);
}