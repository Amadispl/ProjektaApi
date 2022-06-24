using Projekt.Models;

namespace Projekt.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
