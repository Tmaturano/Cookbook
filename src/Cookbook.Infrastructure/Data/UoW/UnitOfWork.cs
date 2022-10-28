using Cookbook.Domain.Interfaces.UoW;

namespace Cookbook.Infrastructure.Data.UoW;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly CookbookContext _context;
    private bool _disposed;

    public UnitOfWork(CookbookContext context) => _context = context;

    public async Task<bool> CommitAsync()
    {
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();

        _disposed = true;
    }
}
