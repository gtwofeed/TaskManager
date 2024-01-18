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

        public UsersController(ApplicationContext db) => 
            this.db = db;

        [HttpGet("test")]
        public IActionResult CreateUser() => Ok("Api = OK!");

        [HttpGet("getusers")]
        public IQueryable<UserDTO> GetUsers() => 
            db.Users.Select(u => u.ToDTO());

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

        [HttpPatch("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (userDTO != null)
            {
                User? user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.Email = userDTO.Email;
                    user.Password = userDTO.Password;
                    user.Status = userDTO.Status;
                    user.FirstName = userDTO.FirstName;
                    user.LastName = userDTO.LastName;
                    user.Phone = userDTO.Phone;
                    user.Photo = userDTO.Photo;

                    db.Users.Update(user);
                    db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }

}
