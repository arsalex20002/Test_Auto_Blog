using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Auto_Blog.DAL.Interfaces;
using Test_Auto_Blog.Domain.Models;

namespace Test_Auto_Blog.DAL.Repositories
{
    public class PostRepository : IMainRepository<Post>
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task Create(Post model)
        {
            await _context.Posts.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts;
        }

        public async Task Delete(Post model)
        {
            _context.Posts.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> Update(Post model)
        {
            _context.Posts.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
