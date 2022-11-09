using AutoMapper;
using Cookbook.Communication.Request;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;
using Cookbook.Exceptions.ExceptionsBase;

namespace Cookbook.Application.UseCases.User.Create;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserUseCase(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(RegisterUserRequest request)
    {
        await ValidateAsync(request);

        var userEntity = _mapper.Map<Domain.Entities.User>(request);
        userEntity.EncryptPassword(request.Password);

        await _userRepository.AddAsync(userEntity);
        await _unitOfWork.CommitAsync();
    }

    private async Task ValidateAsync(RegisterUserRequest request)
    {
        var validator = new CreateUserValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
