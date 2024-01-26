using System.Security.Claims;
using System.Text;
using TaskManager.Api.Models;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class UserService : ICommonService<UserDTO>
    {
        readonly ApplicationContext db;

        public UserService(ApplicationContext db) =>
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

        public User? GetUser(string login, string password) =>
            db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
        public IQueryable<UserDTO> GetAllUsers() =>
            db.Users.Select(u => u.ToDTO());
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
        public bool Сreate(UserDTO userDTO, out int id)
        {
            id = 0;
            if (userDTO != null)
            {
                return ToDo(() =>
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
                    return user.Id;
                }, ref id);
            }
            return false;
        }
        public bool Update(UserDTO userDTO, int id)
        {
            if (userDTO != null)
            {
                User? user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    return ToDo(() =>
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
                    });
                }
                return false;
            }
            return false;
        }
        public bool Delete(int id)
        {
            User? user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return ToDo(() =>
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                });
            }
            return false;
        }

        bool ToDo(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch { return false; }
        }
        bool ToDo(Func<int> func, ref int id)
        {
            try
            {
                id = func.Invoke();
                return true;
            }
            catch { return false; }
        }
    }
}