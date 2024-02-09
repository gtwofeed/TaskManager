using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        readonly UsersService usersServices;
        readonly ProjectsService projectsServices;

        public ProjectsController(ApplicationContext db)
        {
            usersServices = new UsersService(db);
            projectsServices = new ProjectsService(db);
        }

        [HttpGet]
        public IQueryable<ProjectDTO>? GetProjects()
        {
            var user = usersServices.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (user.Status == UserStatus.Admin)
                {
                    return projectsServices.GetAllProjects();
                }
                return projectsServices.GetForAdminById(user.Id).Union(projectsServices.GetForUserById(user.Id));
            }
            return null;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProjectDTO dto)
        {
            if (dto == null) return BadRequest();

            UserDTO? admin = usersServices.GetUser(HttpContext.User.Identity.Name)?.ToDTO();

            if (admin != null)
            {
                if (admin.Status == UserStatus.Admin || admin.Status == UserStatus.Editor)
                {
                    dto.Admin = admin;
                    return projectsServices.Сreate(dto, out int id) ? Ok(id) : BadRequest();
                }
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = usersServices.GetUser(HttpContext.User.Identity.Name);
            var project = projectsServices.Get(id);
            return project != null ? Ok(project) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] ProjectDTO dto)
        {
            if (dto == null) return BadRequest();

            UserDTO? admin = usersServices.GetUser(HttpContext.User.Identity.Name)?.ToDTO();
            if (admin != null)
            {
                if (admin.Status == UserStatus.Admin || admin.Status == UserStatus.Editor)
                    return projectsServices.Update(dto, id) ? Ok() : BadRequest();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = projectsServices.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpPatch("{id}/users/add")]
        public IActionResult AddUsersToProject(int id, [FromBody] int[] userIds)
        {
            if(userIds == null) return BadRequest();

            UserDTO? admin = usersServices.GetUser(HttpContext.User.Identity.Name)?.ToDTO();
            if (admin != null)
            {
                if (admin.Status == UserStatus.Admin || admin.Status == UserStatus.Editor)
                {
                    projectsServices.AddUsersByIds(id, userIds);
                    return Ok();
                }
            }
            return Unauthorized();            
        }

        [HttpPatch("{id}/users/del")]
        public IActionResult DelUsersToProject(int id, [FromBody] int[] userIds)
        {
            if (userIds == null) return BadRequest();

            UserDTO? admin = usersServices.GetUser(HttpContext.User.Identity.Name)?.ToDTO();
            if (admin != null)
            {
                if (admin.Status == UserStatus.Admin || admin.Status == UserStatus.Editor)
                {
                    projectsServices.DelUsersByIds(id, userIds);
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}
