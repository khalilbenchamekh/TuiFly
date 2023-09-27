using TuiFly.Api.Configurations;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// ConfigureServices
// Configure logging
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCorsConfiguration(builder.Configuration);

// WebAPI Config
builder.Services.AddControllers();

// Setting DBContexts
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

// Swagger Config
builder.Services.AddSwaggerConfiguration();

// Adding MediatR for Domain Events and Notifications
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();
// Configure
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.InitializeDbTestDataAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCorsSetup();

app.MapControllers();

app.UseSwaggerSetup();

app.Run();