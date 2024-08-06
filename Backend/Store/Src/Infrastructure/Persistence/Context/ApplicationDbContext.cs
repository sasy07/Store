using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
    {
        
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductBrand> ProductBrand => Set<ProductBrand>();
    public DbSet<ProductType> ProductType => Set<ProductType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDelete);
        modelBuilder.Entity<ProductType>().HasQueryFilter(x => !x.IsDelete);
        modelBuilder.Entity<ProductBrand>().HasQueryFilter(x => !x.IsDelete);
    }
}