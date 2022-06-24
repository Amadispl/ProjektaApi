using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projekt.Entities;
using Projekt.Exceptions;
using Projekt.Models;

namespace Projekt.Services
{
    public class MenuService : IMenuService
    {
        private readonly PizzeriaDbContext _context;
        private readonly IMapper _mapper;
        public MenuService(PizzeriaDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public int Create(int pizzeriaId, CreateMenuDto dto)
        {
            var pizzeria = GetPizzeriaById(pizzeriaId);
            var MenuEntity = _mapper.Map<Menu>(dto);
            MenuEntity.PizzeriaId = pizzeriaId;
            _context.Menus.Add(MenuEntity);
            _context.SaveChanges();
            return MenuEntity.Id;

        }

        public void DeleteAllMenu(int pizzeriaid)
        {
            var pizzeria = GetPizzeriaById(pizzeriaid);
            _context.RemoveRange(pizzeria.Menu);
            _context.SaveChanges();
        }

        public void DeleteMenuById(int pizzeriaid, int menuid)
        {
            var pizzeria = GetPizzeriaById(pizzeriaid);
            var menu = _context.Menus.FirstOrDefault(m => m.PizzeriaId == pizzeria.Id);
            if (menu == null)
            {
                throw new NotFoundException("Menu nie znaleziono");
            }
            _context.Menus.Remove(menu);
            _context.SaveChanges();


        }

        public List<MenuDto> GetAll(int pizzeriaid)
        {
            var pizzeria = GetPizzeriaById(pizzeriaid);
            var menuDtos = _mapper.Map<List<MenuDto>>(pizzeria.Menu);
            return menuDtos;

        }

        public MenuDto GetById(int pizzeriaId, int menuId)
        {
            var pizzeria = GetPizzeriaById(pizzeriaId);
            var menu = _context.Menus.FirstOrDefault(c => c.PizzeriaId == menuId);

            if (menu == null || menu.PizzeriaId != pizzeria.Id)
            {
                throw new NotFoundException("Nie istnieje");
            }
            var menuDto = _mapper.Map<MenuDto>(menu);
            return menuDto;

        }
        private Pizzeria GetPizzeriaById(int pizzeriaid)
        {
            var pizzeria = _context.Pizzerias
                .Include(p => p.Menu)
                .FirstOrDefault(c => c.Id == pizzeriaid);
            if (pizzeria == null)
            {
                throw new NotFoundException("Pizzeria nie istnieje");
            }
            return pizzeria;
        }
    }
}
