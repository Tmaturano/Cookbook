using AutoMapper;
using Cookbook.Application.Services.AutoMapper;
using Cookbook.Application.Services.JWT;
using Cookbook.Application.UseCases.User.Create;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;
using Cookbook.Exceptions.ExceptionsBase;
using FluentAssertions;
using Helper.Requests;
using Moq;
using Xunit;

namespace UseCases.Test.User.Create;

public class CreateUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly Mock<ITokenService> _tokenService;
    private readonly CreateUserUseCase _userUseCase;

    public CreateUserUseCaseTests()
    {
        if (_mapper is null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ResponseToDomainMappingProfile());
            });
            
            var mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }


        _userRepository = new Mock<IUserRepository>();        
        _uow= new Mock<IUnitOfWork>();
        _tokenService= new Mock<ITokenService>();
        _userUseCase = new CreateUserUseCase(_userRepository.Object, _mapper, _uow.Object, _tokenService.Object);
    }


    [Fact]
    public async Task ValidateSuccess()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        _tokenService.Setup(x => x.GenerateToken(It.IsAny<Cookbook.Domain.Entities.User>())).Returns("ABCD");        

        //Act
        var response = await _userUseCase.ExecuteAsync(request);

        //Assert
        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreateUser_WhenEmailAlreadyExists_ShouldReturnError()
    {
        //Arrange
        var request = RegisterUserRequestBuilder.Build();
        _userRepository.Setup(x => x.ExistsAsync(request.Email)).ReturnsAsync(true);

        //Act
        Func<Task> action = async () => { await _userUseCase.ExecuteAsync(request); };

        //Assert
        await action.Should().ThrowAsync<ValidationErrorsException>();
    }
}
