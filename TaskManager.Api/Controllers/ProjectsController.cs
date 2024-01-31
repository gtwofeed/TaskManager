using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models.Services;
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

        [HttpPost]
        public IActionResult Create([FromBody] ProjectDTO dto)
        {
            if (dto == null) return BadRequest();

            dto.Admin = usersServices.GetUser(HttpContext.User.Identity.Name)?.ToDTO();
            if(dto.Admin == null) return NotFound();

            return projectsServices.Сreate(dto, out int id) ? Ok(id) : BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = projectsServices.Get(id);
            return project != null ? Ok(project) : NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] ProjectDTO dto)
        {
            bool result = projectsServices.Update(dto, id);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = projectsServices.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
