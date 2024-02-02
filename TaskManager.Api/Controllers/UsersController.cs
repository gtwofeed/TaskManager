using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        readonly UsersService usersServices;

        public UsersController(ApplicationContext db) =>
            usersServices = new(db);

        [AllowAnonymous]
        [HttpGet("check-status")]
        public IActionResult CheckStatus() =>
            Ok($"Active");

        [HttpGet("all")]
        public IQueryable<UserDTO> GetUsers() =>
            usersServices.GetAllUsers();

        [HttpPost]
        public IActionResult Create([FromBody] UserDTO dto)
        {
            bool result = usersServices.Сreate(dto, out int id); 
            return result ? Ok(id) : BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = usersServices.Get(id);
            return user != null ? Ok(user) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] UserDTO dto)
        {
            bool result = usersServices.Update(dto, id);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = usersServices.Delete(id);
            return result ? Ok() : NotFound();
        }
    }

}
