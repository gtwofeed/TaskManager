using System.Security.Claims;
using System.Text;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectService : ICommonService<ProjectDTO>
    {
        readonly ApplicationContext db;

        public ProjectService(ApplicationContext db) =>
            this.db = db;


        public bool Сreate(ProjectDTO dto, out int id)
        {
            id = 0;
            if (dto is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                Project project = new(dto)
                {
                    
                };
                db.Users.Add(user);
                db.SaveChanges();

                return user.Id;
            }, ref id);
        }

        public ProjectDTO? Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProjectDTO dto, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
