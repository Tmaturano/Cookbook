using Cookbook.Domain.Entities;

namespace Cookbook.Domain.Interfaces.Repository;

public interface ICodeRepository : IRepositoryBase<Code>
{
    Task<Code> GetCodeAsync(string code);
}
