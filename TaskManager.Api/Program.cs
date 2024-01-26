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
            var builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ����� ������������
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // ��������� �������� ApplicationContext � �������� ������� � ����������
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // ���������, ����� �� �������������� �������� ��� ��������� ������
                        ValidateIssuer = true,
                        // ������, �������������� ��������
                        ValidIssuer = AuthOptions.ISSUER,
                        // ����� �� �������������� ����������� ������
                        ValidateAudience = true,
                        // ��������� ����������� ������
                        ValidAudience = AuthOptions.AUDIENCE,
                        // ����� �� �������������� ����� �������������
                        ValidateLifetime = true,
                        // ��������� ����� ������������
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // ��������� ����� ������������
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();


        }        
    }
}
