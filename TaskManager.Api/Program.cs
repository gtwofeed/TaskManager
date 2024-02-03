using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var starup = new Startup(args);
            starup.Run();
        }        
    }
}
