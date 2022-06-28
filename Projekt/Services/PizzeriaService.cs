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
        private readonly IUserContextService _userContextService;


        public PizzeriaService(PizzeriaDbContext dbContext, IMapper mapper, ILogger<PizzeriaService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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
        public PageResult<PizzeriaDto> GetAll(PizzeriaQuery query)
        {
            var result = query.pageSize * (query.pageNumber - 1);
            var baseQuery = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).Where(r => query.searchPhrase == null || (r.Name.ToLower().Contains(query.searchPhrase.ToLower()) || r.Description.ToLower().Contains(query.searchPhrase.ToLower())));

            var pizzeria = baseQuery.Skip(result)
.Take(query.pageSize).ToList();
           var totalItemsCount= baseQuery.Count();


            var pizzeriaDto = _mapper.Map<List<PizzeriaDto>>(pizzeria);
            var finalresult = new PageResult<PizzeriaDto>(pizzeriaDto,totalItemsCount,query.pageSize, query.pageNumber);

            return finalresult;

        }
        public int Create(CreatePizzeriaDto dto)
        {

            var pizzeria = _mapper.Map<Pizzeria>(dto);
            pizzeria.CreatedById = _userContextService.GetUserId;
            _context.Pizzerias.Add(pizzeria);
            _context.SaveChanges();
            return pizzeria.Id;
        }
        public void DeleteById(int id)
        {

            _logger.LogWarning($"Pizzaria with id:{id} DELETE");
            var pizzeria = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).FirstOrDefault(x => x.Id == id);
            if (pizzeria != null)
            {
                var authorization = _authorizationService.AuthorizeAsync(_userContextService.User, pizzeria, new OperationRequirement(OperationRequirementType.Delete)).Result;
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
        public void Edit(int id, UpdatePizzeriaDto dto)
        {


            var pizzeria = _context.Pizzerias.Include(r => r.Address).Include(r => r.Menu).FirstOrDefault(x => x.Id == id);
            if (pizzeria != null)
            {
                pizzeria.Name = dto.Name;
                pizzeria.Description = dto.Description;
                pizzeria.HasDelivery = dto.HasDelivery;
                var authorization = _authorizationService.AuthorizeAsync(_userContextService.User, pizzeria, new OperationRequirement(OperationRequirementType.Update)).Result;
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
