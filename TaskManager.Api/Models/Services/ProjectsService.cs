using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectsService : ICommonService<ProjectDTO>
    {
        readonly ApplicationContext db;

        public ProjectsService(ApplicationContext db) =>
            this.db = db;

        public IQueryable<ProjectDTO> GetAllProjects() =>
            db.Projects.Select(p => p.ToDTO());

        #region CRUD ICommonService
        public bool Сreate(ProjectDTO dto, out int id)
        {
            id = 0;
            if (dto is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                Project project = new(dto) { Admin = GetAdmin(dto) };

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
                project.Photo = dto.Photo;
                project.Status = dto.Status;
                project.Admin = GetAdmin(dto);

                db.Projects.Update(project);
                db.SaveChanges();
            });
        }

        public bool Delete(int id)
        {
            Project? project = db.Projects.FirstOrDefault(p => p.Id == id);
            if (project is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                db.Projects.Remove(project);
                db.SaveChanges();
            });
        }
        #endregion

        User? GetAdmin(ProjectDTO dto)
        {
            if (dto.Admin is null) return null;
            return db.Users.FirstOrDefault(u => u.Id == dto.Admin.Id);
        }
    }
}
