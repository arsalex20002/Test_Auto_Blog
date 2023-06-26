using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.DAL.Repositories;
using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Service.Implementations;
using Test_Auto_Blog.Service.Interfaces;

namespace Test_Auto_Blog
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMainRepository<Car>, CarRepository>();
            services.AddTransient<IMainRepository<User>, UserRepository>();
            services.AddTransient<IMainRepository<Post>, PostRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
        }
    }
}