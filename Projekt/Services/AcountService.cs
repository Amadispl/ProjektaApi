using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projekt.Entities;
using Projekt.Exceptions;
using Projekt.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projekt.Services
{
    public class AccountService : IAccountService
    {
        private readonly PizzeriaDbContext _context;
        private readonly IPasswordHasher<User> _hasher;
        private readonly AuthenticationSettings _settings;
        public AccountService(PizzeriaDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _hasher = passwordHasher;
            _settings = authenticationSettings;
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}".ToString()),
                new Claim(ClaimTypes.Role,$"{user.Role.Name}"),
                new Claim("DateOfBirth",user.DateofBirth.Value.ToString("yy-MM-dd")),


            };
            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(new Claim("Nationality", user.Nationality));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_settings.JwtExpireDays);
            var token = new JwtSecurityToken(_settings.JwtIssuer, _settings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var user = new User()
            {
                Email = dto.Email,
                DateofBirth = dto.DateofBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}
