using Projekt.Models;

namespace Projekt.Services
{
    public interface IMenuService
    {
        int Create(int pizzeriaId, CreateMenuDto dto);
        MenuDto GetById(int pizzeriaId, int menuId);
        List<MenuDto> GetAll(int pizzeriaid);
        void DeleteAllMenu(int pizzeriaid);
        void DeleteMenuById(int pizzeriaid, int menuid);
    }
}
