using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL.Interfaces
{
    public interface IMainRepository<T>
    {
        Task<bool> Create(T model);

		Task<bool> Delete(T model);

        Task<List<Car>> Select();

        Task<Car> GetById(int id);
        

    }
}
