using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TaskManager.Api.Models;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly ApplicationContext db;

        public UsersController(ApplicationContext db) => this.db = db;

        [HttpGet("test")]
        public IActionResult CreateUser() => Ok("Api = OK!");

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO != null)
            {
                User user = new()
                {
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    Status = userDTO.Status,
                    RegistrationDate = userDTO.RegistrationDate,
                    Phone = userDTO.Phone,
                    Photo = userDTO.Photo,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    LastLoginDate = userDTO.LastLoginDate,
                };
                db.Users.Add(user);
                db.SaveChanges();
                return Ok(user.Id);
            }
            return BadRequest();
        }
    }

}
