using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using Projekt.Services;
using System.Security.Claims;

namespace PizzeriaApi.Controllers
{
    [Route("api/pizzeria")]
    [Authorize]
    [ApiController]
    public class PizzeriaController : Controller
    {
        private readonly IPizzeriaService _pizzeriaservice;

        public PizzeriaController(IPizzeriaService pizzeriaService)
        {
            _pizzeriaservice = pizzeriaService;
        }
        [HttpGet]
        
        public ActionResult<IEnumerable<PizzeriaDto>> GetAll([FromQuery]PizzeriaQuery query)
        {

            return Ok(_pizzeriaservice.GetAll(query));
        }
        [HttpGet("{id}")]

        public ActionResult<PizzeriaDto> Get([FromRoute] int id)
        {



            var pizzeria = _pizzeriaservice.GetById(id);
            return Ok(pizzeria);



        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteById([FromRoute] int id)
        {

            _pizzeriaservice.DeleteById(id);

            return Ok();

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Create([FromBody] CreatePizzeriaDto dto)
        {

           
            var id = _pizzeriaservice.Create(dto);
            return Created($"api/pizzeria/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Edit([FromRoute] int id, [FromBody] UpdatePizzeriaDto dto)
        {
            _pizzeriaservice.Edit(id, dto);
            return Ok();
        }

    }
}
