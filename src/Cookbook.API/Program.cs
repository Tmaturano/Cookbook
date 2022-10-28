using Cookbook.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureMvc();
builder.ConfigureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

app.Run();
