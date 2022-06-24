
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Projekt;
using Projekt.Entities;
using Projekt.Middleware;
using Projekt.Models;
using Projekt.Models.Validators;
using Projekt.Properties.Authorization;
using Projekt.Services;
using System.Reflection;
using System.Text;


// Add services to the container.
var authenticationSetting = new AuthenticationSettings();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey)),
    };
});
builder.Services.AddAuthorization();
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

builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzeria Api");

});
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<PizzeriaSeeder>();
seeder.Seed();
app.UseAuthorization();

app.MapControllers();

app.Run();
