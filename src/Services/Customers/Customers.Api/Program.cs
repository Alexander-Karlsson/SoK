using Customers.Infrastructure;
using System.Text.Json.Serialization;
using Customers.Api.Exceptions;

var builder = WebApplication.CreateBuilder(args);


// Infrastructure-lagret via DependencyInjection-klassen i Customers.Infrastructure-projektet
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(
             new JsonStringEnumConverter());
     });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<CustomerExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
