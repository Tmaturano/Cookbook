using Cookbook.API.Filters;
using Cookbook.Application;
using Cookbook.Application.Services.AutoMapper;
using Cookbook.Application.Services.JWT;
using Cookbook.Application.UseCases.User.Create;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;
using Cookbook.Infrastructure.Data;
using Cookbook.Infrastructure.Data.Repository;
using Cookbook.Infrastructure.Data.UoW;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Cookbook.API.Extensions
{
    public static class AppExtensions
    {
        public static void LoadConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey");
            Configuration.ApiKeyName = builder.Configuration.GetValue<string>("ApiKeyName");
            Configuration.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
            Configuration.TokenDurationInMinutes = builder.Configuration.GetValue<int>("TokenDurationInMinutes");
        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            bool.TryParse(builder.Configuration.GetValue<string>("InMemoryDatabase"), out var inMemoryDatabase);

            if (!inMemoryDatabase)
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<CookbookContext>(options => options.UseNpgsql(connectionString));
            }

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperConfiguration)));
            builder.Services.AddScoped<ITokenService, TokenService>();

            //Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();            

            //UseCases
            builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        }

        public static void ConfigureMvc(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();

            builder.Services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            builder
                .Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; }) //disable the default model state behavior, so will be needed to call ModelState.IsValid
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                });
        }
    }
}
