using AutoMapper;
using Cookbook.Communication.Request;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Exceptions.ExceptionsBase;

namespace Cookbook.Application.UseCases.User.Create;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(RegisterUserRequest request)
    {
        await ValidateAsync(request);

        var userEntity = _mapper.Map<Domain.Entities.User>(request);

        await _userRepository.AddAsync(userEntity);
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
