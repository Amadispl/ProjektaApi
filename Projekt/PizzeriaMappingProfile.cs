using AutoMapper;
using Projekt.Entities;
using Projekt.Models;

namespace PizzeriaApi
{
    public class PizzeriaMappingProfile : Profile
    {
        public PizzeriaMappingProfile()
        {
            CreateMap<Pizzeria, PizzeriaDto>().ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
              .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
            .ForMember(m => m.Number, c => c.MapFrom(s => s.Address.Number))
            .ForMember(m => m.Postalcode, c => c.MapFrom(s => s.Address.PostalCode)); ;
            CreateMap<Menu, MenuDto>();
            CreateMap<CreatePizzeriaDto, Pizzeria>().ForMember(m => m.Address, c => c.MapFrom(dto => new Address() { City = dto.City, PostalCode = dto.Postalcode, Street = dto.Street, Number = dto.Anumber }));
            CreateMap<CreateMenuDto, Menu>();


        }
    }
}
