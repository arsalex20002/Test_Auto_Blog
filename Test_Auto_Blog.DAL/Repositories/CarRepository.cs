using Microsoft.EntityFrameworkCore;
using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<bool> Create(Car model)
        {
           _context.Cars.Add(model);
           await _context.SaveChangesAsync();

           return true;
        }

        public async Task<bool> Delete(Car model)
        {
            _context.Cars.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Car> GetById(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

		public async Task<Car> GetByName(string name)
        {
			return await _context.Cars.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<List<Car>> Select()
        {
            return await _context.Cars.ToListAsync();
        }
    }
}
