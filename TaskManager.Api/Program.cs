using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // получаем строку подключения из файла конфигурации
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
