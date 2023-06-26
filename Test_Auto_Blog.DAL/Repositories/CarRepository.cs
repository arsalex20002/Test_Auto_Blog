using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL.Repositories
{
    public class CarRepository : IMainRepository<Car>
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task Create(Car model)
        {
            await _context.Cars.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Car> GetAll()
        {
            return _context.Cars;
        }

        public async Task Delete(Car model)
        {
            _context.Cars.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> Update(Car model)
        {
            _context.Cars.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}