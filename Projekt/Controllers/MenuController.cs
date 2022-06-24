using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/pizzeria/{pizzeriaid}/menu")]
    [Authorize]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMenuService _service;
        public MenuController(IMenuService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Post([FromRoute] int pizzeriaid, CreateMenuDto dto)
        {
            var menuId = _service.Create(pizzeriaid, dto);
            return Created($"api/pizzeria/{pizzeriaid}/menu/{menuId}", null);
        }
        [HttpGet("{menuid}")]
        public ActionResult<MenuDto> GetById([FromRoute] int pizzeriaid, [FromRoute] int menuid)
        {
            var menu = _service.GetById(pizzeriaid, menuid);
            return Ok(menu);
        }
        [HttpGet]
        public ActionResult<IEnumerable<MenuDto>> GetAll([FromRoute] int pizzeriaid)
        {
            return Ok(_service.GetAll(pizzeriaid));

        }
        [HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete([FromRoute] int pizzeriaid)
        {
            _service.DeleteAllMenu(pizzeriaid);
            return Ok();
        }
        [HttpDelete("{menuid}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteById([FromRoute] int pizzeriaid, [FromRoute] int menuid)
        {
            _service.DeleteMenuById(pizzeriaid, menuid);
            return Ok();
        }
    }
}


