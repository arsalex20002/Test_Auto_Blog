using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL.Repositories
{
    public class UserRepository : IMainRepository<User>
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task Delete(User model)
        {
            _context.Users.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Create(User model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Update(User model)
        {
            _context.Users.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
