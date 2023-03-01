using Cookbook.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data.Repository;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly CookbookContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(CookbookContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }

    public virtual async Task AddAsync(TEntity obj) => await DbSet.AddAsync(obj);

    public async Task<bool> ExistsAsync(Guid id) => await DbSet.FindAsync(id) is not null;

    public void Update(TEntity obj) => DbSet.Update(obj);

    public async Task<TEntity> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);
}
