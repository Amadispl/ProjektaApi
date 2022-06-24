using Projekt.Models;
using System.Security.Claims;

namespace Projekt.Services
{
    public interface IPizzeriaService
    {
        int Create(CreatePizzeriaDto dto, int id);
        IEnumerable<PizzeriaDto> GetAll();
        PizzeriaDto GetById(int id);
        void DeleteById(int id, ClaimsPrincipal user);
        void Edit(int id, UpdatePizzeriaDto dto, ClaimsPrincipal user);
    }
}