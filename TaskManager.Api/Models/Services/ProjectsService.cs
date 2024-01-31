using System.Security.Claims;
using System.Text;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectsService : ICommonService<ProjectDTO>
    {
        readonly ApplicationContext db;

        public ProjectsService(ApplicationContext db) =>
            this.db = db;


        #region CRUD ICommonService
        public bool Сreate(ProjectDTO dto, out int id)
        {
            id = 0;
            if (dto is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                Project project = new(dto);
                db.Projects.Add(project);
                db.SaveChanges();

                return project.Id;
            }, ref id);
        }

        public ProjectDTO? Get(int id) =>
            db.Projects.FirstOrDefault(p => p.Id == id)?.ToDTO();

        public bool Update(ProjectDTO dto, int id)
        {
            if (dto is null) return false;

            Project? project = db.Projects.FirstOrDefault(p => p.Id == id);
            if (project is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                project.Name = dto.Name;
                project.Description = dto.Description;
                project.Status = dto.Status;
                project.AdminId = project.AdminId;
                project.Admin = null;

                db.Projects.Update(project);
                db.SaveChanges();
            });
        }
        public bool Delete(int id)
        {
            User? user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                db.Users.Remove(user);
                db.SaveChanges();
            });
        }
        #endregion
    }
}
