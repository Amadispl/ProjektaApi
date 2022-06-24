using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Projekt;
using Projekt.Entities;
using Projekt.Services;
using System.Reflection;

var authenticationSetting = new AuthenticationSettings();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzeriaDbContext>();
builder.Services.AddScoped<PizzeriaSeeder>();
builder.Services.AddScoped<IPizzeriaService, PizzeriaService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddSingleton(authenticationSetting);
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IAuthorizationHandler, OperationHandler>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<PizzeriaSeeder>();
seeder.Seed();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
