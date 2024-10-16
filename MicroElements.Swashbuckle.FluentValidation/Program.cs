using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddressValidator>();
builder.Services.AddSwaggerGen();
builder.AddFluentValidationEndpointFilter();


builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/address", (Address address) =>
{
    Console.WriteLine(address);
})
.AddFluentValidationFilter()
.WithOpenApi();

app.Run();


public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.StreetName).MinimumLength(2).NotEmpty();
    }
}

public record Address
(
    string StreetName
);