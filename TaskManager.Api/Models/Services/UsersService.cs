using System.Security.Claims;
using System.Text;
using TaskManager.Api.Models;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class UsersService : ICommonService<UserDTO>
    {
        readonly ApplicationContext db;

        public UsersService(ApplicationContext db) =>
            this.db = db;

        public (string, string) GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string username = "";
            string password = "";
            string authHeader = request.Headers.Authorization.ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic"))
            {
                string encodedUserNamePass = authHeader.Replace("Basic", "");
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");

                string[] usernamePassArry = encoding.GetString(Convert.FromBase64String(encodedUserNamePass)).ToString().Split(':');
                (username, password) = (usernamePassArry.First(), usernamePassArry.Last());
            }
            return (username, password);
        }
        public ClaimsIdentity? GetIdentity(string username, string password)
        {
            User? currentUser = GetUser(username, password);
            if (currentUser != null)
            {
                currentUser.LastLoginDate = DateTime.Now;
                db.Users.Update(currentUser);
                db.SaveChanges();

                List<Claim> claims =
                [
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Status.ToString())
                ];

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        public User? GetUser(string login, string password) =>
            db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
        public User? GetUser(string login) =>
            db.Users.FirstOrDefault(u => u.Email == login);

        public IQueryable<UserDTO> GetAllUsers() =>
            db.Users.Select(u => u.ToDTO());

        #region CRUD ICommonService
        public bool Сreate(UserDTO dto, out int id)
        {
            id = 0;
            if (dto is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                User user = new(dto);
                db.Users.Add(user);
                db.SaveChanges();

                return user.Id;
            }, ref id);
        }
        public UserDTO? Get(int id) =>
            db.Users.FirstOrDefault(u => u.Id == id)?.ToDTO();
        public bool Update(UserDTO dto, int id)
        {
            if (dto is null) return false;

            User? user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user is null) return false;

            return InvokeExtensions.ToDo(() =>
            {
                user.Email = dto.Email;
                user.Password = dto.Password;
                user.Status = dto.Status;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Phone = dto.Phone;
                user.Photo = dto.Photo;

                db.Users.Update(user);
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


        public IEnumerable<UserDTO> GetByIds(int[] userIds)
        {
            foreach (int userId in userIds)
            {
                yield return db.Users.Find(userId)?.ToDTO();
            }
        }
    }
}