using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;

namespace Cookbook.Application.UseCases.Connection;

public class GenerateQrCodeUseCase : IGenerateQrCodeUseCase
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICodeRepository _codeRepository;

    public GenerateQrCodeUseCase(IAuthenticatedUser authenticatedUser, IUnitOfWork unitOfWork, ICodeRepository codeRepository)
    {
        _authenticatedUser = authenticatedUser;
        _unitOfWork = unitOfWork;
        _codeRepository = codeRepository;
    }

    public async Task<(string QrCode, string UserId)> ExecuteAsync()
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();

        var code = new Code(Guid.NewGuid().ToString(), authenticatedUser.Id);

        await _codeRepository.AddAsync(code);
        await _unitOfWork.CommitAsync();

        return (code.Value, code.UserId.ToString());
    }
}
