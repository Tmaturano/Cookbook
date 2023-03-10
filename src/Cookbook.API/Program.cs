using Cookbook.API.Extensions;
using Cookbook.API.Filters;
using Cookbook.API.Middleware;
using Cookbook.API.WebSockets;
using Cookbook.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls= true);
builder.Services.AddHttpContextAccessor();
builder.ConfigureMvc();
builder.ConfigureServices();
builder.LoadConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Cookbook API", Version= "1.0" });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header with Bearer scheme. Example: \"Authorization: Bearer {token}\""
    }) ;
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IAuthorizationHandler, AuthenticatedUserHandler>();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AuthenticatedUser", policy => policy.Requirements.Add(new AuthenticatedUserRequirement()));
});

builder.Services.AddScoped<AuthenticatedUserAttribute>();

builder.Services.AddSignalR();
builder.Services.AddHealthChecks().AddDbContextCheck<CookbookContext>();


var app = builder.Build();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [ HealthStatus.Healthy ] = StatusCodes.Status200OK,        
        [ HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.MapHub<AddConnection>("/addConnection");

app.Run();


public partial class Program 
{
}
