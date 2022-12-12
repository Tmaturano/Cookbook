using Cookbook.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook.API.Test;

public class WebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //using the appsettings.Test.json and work with InMemory Database for the tests
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(services=> services.ServiceType == typeof(CookbookContext));
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<CookbookContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDatabaseTesting");
                    options.UseInternalServiceProvider(provider);
                });

                var serviceProvider = services.BuildServiceProvider();
                
                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;

                var database = scopeService.GetRequiredService<CookbookContext>(); 
                database.Database.EnsureDeleted();

            });
    }
}
