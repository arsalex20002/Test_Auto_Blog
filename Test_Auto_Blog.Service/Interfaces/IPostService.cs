using Test_Auto_Blog.Domain.Models;
using Test_Auto_Blog.Domain.Response;
using Test_Auto_Blog.Domain.ViewModels.Post;

namespace Test_Auto_Blog.Service.Interfaces
{
    public interface IPostService
    {
        Task<IBaseResponse<IEnumerable<Post>>> GetPosts(string Name = null, string Type = null, int PostDate = 0, int CarDate = 0);
        Task<IBaseResponse<IEnumerable<Post>>> GetPostsByName(string name);
        public Task<IBaseResponse<Post>> GetPostById(int id);
        Task<IBaseResponse<PostViewModel>> GetPost(int id);
        Task<IBaseResponse<Post>> Create(PostViewModel car, string userName);
        Task<IBaseResponse<bool>> DeletePost(int id);
        Task<IBaseResponse<Post>> Edit(int id, PostViewModel model);
    }
}
