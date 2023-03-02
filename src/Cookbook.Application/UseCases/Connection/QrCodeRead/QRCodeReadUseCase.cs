using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Communication.Response;
using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;

namespace Cookbook.Application.UseCases.Connection.QrCodeRead;

public class QRCodeReadUseCase : IQRCodeReadUseCase
{
    private readonly ICodeRepository _codeRepository;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IAuthenticatedUser _authenticatedUser;

    public QRCodeReadUseCase(ICodeRepository codeRepository, 
        IAuthenticatedUser authenticatedUser, 
        IConnectionRepository connectionRepository)
    {
        _codeRepository = codeRepository;
        _authenticatedUser = authenticatedUser;
        _connectionRepository = connectionRepository;
    }

    public async Task<(UserConnectionResponse UserToConnect, string OwnerQrCdeId)> ExecuteAsync(string connectionCode)
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();
        var code = await _codeRepository.GetCodeAsync(connectionCode);

        await ValidateAsync(code, authenticatedUser);

        var userToConnect = new UserConnectionResponse(authenticatedUser.Name);

        return (userToConnect, code.UserId.ToString());
    }

    private async Task ValidateAsync(Code code, Domain.Entities.User user)
    {
        if (code is null)
        {
            throw new InvalidOperationException("");
        }

        if (code.UserId == user.Id)
        {
            throw new InvalidOperationException("");
        }

        var existsConnection = await _connectionRepository.ExistsBetween(user.Id, code.UserId);
        if (existsConnection)
        {
            throw new InvalidOperationException("");
        }
    }
}
