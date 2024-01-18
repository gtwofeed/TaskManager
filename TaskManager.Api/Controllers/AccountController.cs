﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly ApplicationContext db;
        readonly UserServices userServices;

        public AccountController(ApplicationContext db, UserServices userServices)
        {
            this.userServices = userServices;
            this.db = db;
        }

        [HttpGet("test")]
        public IActionResult CreateUser() =>
            Ok("AccountController = OK!");

        [Authorize]
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            string username = HttpContext.User.Identity.Name;
            if(username != null)
            {
                User? user = db.Users.FirstOrDefault(u => u.Email == username);
                if(user != null)
                {
                    return Ok(user.Id);
                }
                return NotFound();
            }
            return BadRequest("Context Not Found");
        }

        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var userData = userServices.GetUserLoginPassFromBasicAuth(Request);
            var login = userData.Item1;
            var pass = userData.Item2;
            var identity = userServices.GetIdentity(login, pass);

            if (identity == null)
                return BadRequest("Неправильный логин или пароль");

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("update")]
        public IActionResult UpdateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO != null)
            {
                string usetname = HttpContext.User.Identity.Name;
                User? user = db.Users.FirstOrDefault(u => u.Email == usetname);
                if (user != null)
                {
                    user.Password = userDTO.Password;
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
    }
}