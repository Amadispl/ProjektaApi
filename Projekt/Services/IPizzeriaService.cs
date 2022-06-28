using Projekt.Models;
using System.Security.Claims;

namespace Projekt.Services
{
    public interface IPizzeriaService
    {
        int Create(CreatePizzeriaDto dto);
        PageResult<PizzeriaDto> GetAll(PizzeriaQuery query);
        PizzeriaDto GetById(int id);
        void DeleteById(int id);
        void Edit(int id, UpdatePizzeriaDto dto);
    }
}