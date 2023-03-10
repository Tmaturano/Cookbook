using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;

namespace Cookbook.Application.UseCases.Connection.AcceptConnection;

public class AcceptConnectionUseCase : IAcceptConnectionUseCase
{
    private readonly ICodeRepository _codeRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionRepository _connectionRepository;

    public AcceptConnectionUseCase(ICodeRepository codeRepository, 
        IAuthenticatedUser authenticatedUser, 
        IUnitOfWork unitOfWork, 
        IConnectionRepository connectionRepository)
    {
        _codeRepository = codeRepository;
        _authenticatedUser = authenticatedUser;
        _unitOfWork = unitOfWork;
        _connectionRepository = connectionRepository;
    }

    public async Task<string> ExecuteAsync(Guid userToConnectId)
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();
        await _codeRepository.DeleteAsync(authenticatedUser.Id);

        await _connectionRepository.RegisterAsync(new Domain.Entities.Connection
        {
            UserId = authenticatedUser.Id,
            ConnectedWithUserId = userToConnectId
        });

        await _unitOfWork.CommitAsync();

        return authenticatedUser.Id.ToString();
    }
}
