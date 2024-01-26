using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
using TaskManager.Common.Models;
using Task = TaskManager.Api.Models.Task;

namespace TaskManager.Api
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Desk> Desks { get; set; } = null!;
        public DbSet<Task> Tasks { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            if (Users.Any(u => u.Status == UserStatus.Admin) == false)
            {
                User user = new()
                {
                    Email = "fistadmin",
                    Password = "admin",
                    Status = UserStatus.Admin,
                };
                Users.Add(user);
                SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Admin)
                .WithMany()
                .HasForeignKey("AdminId");

            modelBuilder.Entity<Task>()
                .HasOne(t => t.Creater)
                .WithMany()
                .HasForeignKey("CreaterId");

            modelBuilder.Entity<Task>()
                .HasOne(t => t.Executor)
                .WithMany()
                .HasForeignKey("ExecutorId");
        }
    }
}
