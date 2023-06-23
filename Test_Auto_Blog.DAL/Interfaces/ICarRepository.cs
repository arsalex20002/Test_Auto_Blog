using Test_Auto_Blog.Domain.Models;
namespace Test_Auto_Blog.DAL.Interfaces 
{
    public interface ICarRepository : IMainRepository<Car>
    {
		Task<Car> GetByName(string name);
    }
}
