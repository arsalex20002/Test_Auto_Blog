using Microsoft.EntityFrameworkCore;
using Test_Auto_Blog.Domain.Enum;
using Test_Auto_Blog.Domain.Helpers;
using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
        { 
            Database.EnsureCreated();
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AutoBlog;Username=postgres;Password=123123");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User[]
            {
                new User { Id = 1, Name = "Main", Password = HashPasswordHelper.HashPassowrd("123123"), Email = "fortun2@list.ru", Role = Role.Admin},
                new User { Id = 2, Name = "Alex", Password = HashPasswordHelper.HashPassowrd("123123"), Email = "fortun3@list.ru", Role = Role.User},
                new User { Id = 3, Name = "Petr", Password = HashPasswordHelper.HashPassowrd("123123"), Email = "fortun4@list.ru", Role = Role.User},
            });
            modelBuilder.Entity<Car>().HasData(
            new Car[]
            {
                new Car { Id = 1, Name = "BMW X6", Description = "Love", DateCreate = DateTime.Now, TypeCar = TypeCar.SportCar},
                new Car { Id = 2, Name = "BMW X5", Description = "Love", DateCreate = DateTime.Now, TypeCar = TypeCar.Bus},
                new Car { Id = 3, Name = "Porshe Panamera", Description = "Love", DateCreate = DateTime.Now, TypeCar = TypeCar.SportCar},
                new Car { Id = 4, Name = "Audi R8", Description = "Love", DateCreate = DateTime.Now, TypeCar = TypeCar.Sedan},
                new Car { Id = 5, Name = "Camri 2-5", Description = "Love", DateCreate = DateTime.Now, TypeCar = TypeCar.Truck},
            });
            byte[] a = { 1, 0, 1, 0 };
            modelBuilder.Entity<Post>().HasData(
            new Post[]
            {
                new Post { Id = 1, Name = "BMW пост", Description = "Love", DateCreate = DateTime.Now, EditTime = DateTime.Now, UserId = 1, CarId = 1, IsPublic = true,Avatar = a},
                new Post { Id = 2, Name = "BMW пост", Description = "Love", DateCreate = DateTime.Now, EditTime = DateTime.Now, UserId = 2, CarId = 2, IsPublic = true,Avatar = a},
                new Post { Id = 3, Name = "BMW пост", Description = "Love", DateCreate = DateTime.Now, EditTime = DateTime.Now, UserId = 3, CarId = 3, IsPublic = true,Avatar = a},
                new Post { Id = 4, Name = "BMW пост", Description = "Love", DateCreate = DateTime.Now, EditTime = DateTime.Now, UserId = 1, CarId = 4, IsPublic = true,Avatar = a},
                new Post { Id = 5, Name = "BMW пост", Description = "Love", DateCreate = DateTime.Now, EditTime = DateTime.Now, UserId = 2, CarId = 5, IsPublic = true,Avatar = a},
            });
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            });
        }
    }
}
