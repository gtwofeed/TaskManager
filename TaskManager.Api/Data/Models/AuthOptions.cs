﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManager.Api.Data.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretsecretsecretkey!321";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
