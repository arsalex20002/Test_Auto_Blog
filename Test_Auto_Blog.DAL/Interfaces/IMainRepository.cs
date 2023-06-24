using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL.Interfaces
{
    public interface IMainRepository<T>
    {
        Task<bool> Create(T model);

		Task<bool> Delete(T model);

        Task<List<T>> Select();

        Task<T> GetById(int id);

        Task<T> Update(T model);
        

    }
}
