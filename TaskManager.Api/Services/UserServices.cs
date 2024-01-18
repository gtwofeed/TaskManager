using System.Security.Claims;
using System.Text;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services
{
    public class UserServices
    {
        readonly ApplicationContext db;

        public UserServices(ApplicationContext db) =>
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
    }
}
