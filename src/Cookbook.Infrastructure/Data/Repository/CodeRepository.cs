using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data.Repository;

public class CodeRepository : RepositoryBase<Code>, ICodeRepository
{
    public CodeRepository(CookbookContext context) : base(context)
    {        
    }

    public override async Task AddAsync(Code obj)
    {
        var code = await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == obj.UserId);
        if (code is null)
        {
            await base.AddAsync(obj);
            return;            
        }

        code.SetValue(obj.Value);
        Update(code);
    }
}
