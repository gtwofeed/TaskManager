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
        readonly UserService userServices;

        public UsersController(ApplicationContext db) =>
            userServices = new(db);

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test() =>
            Ok("Test - ok");

        [HttpGet("all")]
        public IQueryable<UserDTO> GetUsers() =>
            userServices.GetAllUsers();

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            bool result = userServices.Сreate(userDTO, out int id); 
            return result ? Ok(id) : BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            bool result = userServices.Update(userDTO, id);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool result = userServices.Delete(id);
            return result ? Ok() : NotFound();
        }
    }

}
