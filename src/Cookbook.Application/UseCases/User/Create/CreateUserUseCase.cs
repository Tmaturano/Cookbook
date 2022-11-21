using AutoMapper;
using Cookbook.Application.Services.JWT;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;
using Cookbook.Exceptions;
using Cookbook.Exceptions.ExceptionsBase;
using FluentValidation.Results;
using SecureIdentity.Password;

namespace Cookbook.Application.UseCases.User.Create;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public CreateUserUseCase(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<UserRegisteredResponse> ExecuteAsync(RegisterUserRequest request)
    {
        await ValidateAsync(request);

        var userEntity = _mapper.Map<Domain.Entities.User>(request);

        var encryptedPassword = PasswordHasher.Hash(request.Password);

        userEntity.SetPassword(encryptedPassword);

        await _userRepository.AddAsync(userEntity);
        await _unitOfWork.CommitAsync();

        return new UserRegisteredResponse(Token: _tokenService.GenerateToken(userEntity));
    }

    private async Task ValidateAsync(RegisterUserRequest request)
    {
        var validator = new CreateUserValidator();
        var result = await validator.ValidateAsync(request);

        if (await _userRepository.ExistsAsync(request.Email))        
            result.Errors.Add(new ValidationFailure("email", ErrorMessages.EmailAlreadyExists));        

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
