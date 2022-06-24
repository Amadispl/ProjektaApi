using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Projekt.Entities;
using Projekt.Exceptions;
using Projekt.Models;
using Projekt.Properties.Authorization;
using System.Security.Claims;

namespace Projekt.Services
{
    public class PizzeriaService : IPizzeriaService
    {
        private readonly PizzeriaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;


        public PizzeriaService(PizzeriaDbContext dbContext, IMapper mapper, ILogger<PizzeriaService> logger, IAuthorizationService authorizationService)
        {
            _context = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }
        public PizzeriaDto GetById(int id)
        {

            var pizzeria = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).FirstOrDefault(x => x.Id == id);

            if (null == pizzeria)
            {
                throw new NotFoundException("Pizzeria not found");
            }

            else
            {
                var pizzeriaDto = _mapper.Map<PizzeriaDto>(pizzeria);
                return pizzeriaDto;
            }
        }
        public IEnumerable<PizzeriaDto> GetAll()
        {

            var pizzeria = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).ToList();
            var pizzeriaDto = _mapper.Map<List<PizzeriaDto>>(pizzeria);
            return pizzeriaDto;

        }
        public int Create(CreatePizzeriaDto dto, int id)
        {

            var pizzeria = _mapper.Map<Pizzeria>(dto);
            pizzeria.CreatedById = id;
            _context.Pizzerias.Add(pizzeria);
            _context.SaveChanges();
            return pizzeria.Id;
        }
        public void DeleteById(int id, ClaimsPrincipal user)
        {

            _logger.LogWarning($"Pizzaria with id:{id} DELETE");
            var pizzeria = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).FirstOrDefault(x => x.Id == id);
            if (pizzeria != null)
            {
                var authorization = _authorizationService.AuthorizeAsync(user, pizzeria, new OperationRequirement(OperationRequirementType.Delete)).Result;
                if (!authorization.Succeeded)
                {
                    throw new ForbidException("Forbidden");
                }
                _context.Pizzerias.Remove(pizzeria);
                _context.SaveChanges();

            }
            else
                throw new NotFoundException("Pizzeria not found");

        }
        public void Edit(int id, UpdatePizzeriaDto dto, ClaimsPrincipal user)
        {


            var pizzeria = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).FirstOrDefault(x => x.Id == id);
            if (pizzeria != null)
            {
                pizzeria.Name = dto.Name;
                pizzeria.Description = dto.Description;
                pizzeria.HasDelivery = dto.HasDelivery;
                var authorization = _authorizationService.AuthorizeAsync(user, pizzeria, new OperationRequirement(OperationRequirementType.Update)).Result;
                if (!authorization.Succeeded)
                {
                    throw new ForbidException("Forbidden");
                }
                _context.SaveChanges();

            }
            else
                throw new NotFoundException("Pizzeria not found");
        }
    }
}
