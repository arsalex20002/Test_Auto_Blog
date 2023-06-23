using Microsoft.EntityFrameworkCore;
using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public ApplicationDbContext() 
        { 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AutoBlog;Username=postgres;Password=123123");
        }
    }
}
