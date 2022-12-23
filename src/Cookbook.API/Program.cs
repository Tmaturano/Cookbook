using Cookbook.API.Extensions;
using Cookbook.API.Filters;
using Cookbook.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls= true);
builder.Services.AddHttpContextAccessor();
builder.ConfigureMvc();
builder.ConfigureServices();
builder.LoadConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthenticatedUserAttribute>();

var app = builder.Build();

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
app.Run();


public partial class Program 
{
}
