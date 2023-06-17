using app.shoppingpromotions.host;
using app.shoppingpromotions.host.Filters;
using app.shoppingpromotions.host.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDependencies(builder.Configuration, builder.Environment.IsDevelopment());
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>(); // Register the custom exception filter
})
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ShoppingRequestValidator>());
#pragma warning restore CS0618 // Type or member is obsolete

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
