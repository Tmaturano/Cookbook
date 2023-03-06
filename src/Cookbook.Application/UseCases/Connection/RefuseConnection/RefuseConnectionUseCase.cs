﻿using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;

namespace Cookbook.Application.UseCases.Connection.RefuseConnection;

public class RefuseConnectionUseCase : IRefuseConnectionUseCase
{
    private readonly ICodeRepository _codeRepository;    
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IUnitOfWork _unitOfWork;

    public RefuseConnectionUseCase(ICodeRepository codeRepository, IAuthenticatedUser authenticatedUser, IUnitOfWork unitOfWork)
    {
        _codeRepository = codeRepository;
        _authenticatedUser = authenticatedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync()
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();

        await _codeRepository.DeleteAsync(authenticatedUser.Id);
        await _unitOfWork.CommitAsync();

        return authenticatedUser.Id;
    }
}