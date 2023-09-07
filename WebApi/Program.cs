using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Repositories;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(Program).GetTypeInfo().Assembly);
builder.Services.AddDbContext<GwpDbContext>(options => options.UseInMemoryDatabase("CountryGwt"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICountryGwpService, CountryGwpService>();
builder.Services.AddTransient<ICountryGwpRepository, CountryGwpRepository>();
builder.Services.AddTransient<IDbInitializer, DbInitializer>();



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    var context = scope.ServiceProvider.GetRequiredService<GwpDbContext>();
    dbInitializer.Initialize(context);
}

app.MapControllers();

app.Run();
