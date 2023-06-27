using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_Auto_Blog.Domain.Enum;
using Test_Auto_Blog.Domain.ViewModels.Post;
using Test_Auto_Blog.Service.Interfaces;
namespace Test_Auto_Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly ICarService _carService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        public PostController(IPostService postService,ICarService carService, IUserService userService)
        {
            _postService = postService;
            _carService = carService;
            _userService = userService;
        }
        public async Task<IActionResult> Delete(int id)
        {
            if(User.Identity.IsAuthenticated)
            {
                var responce_post = await _postService.GetPostById(id);
                var responce_user = await _userService.GetUserByName(User.Identity.Name);
                
                if (responce_post.Data.UserId == responce_user.Data.Id || User.IsInRole("Admin"))
                {
                    var response_post_delete = await _postService.DeletePost(id);
                    if (response_post_delete.Status == Domain.Enum.ErrorStatus.Success)
                    {
                        return RedirectToAction("GetPosts","Post");
                    }
                    else
                    {
                        return View("Error", $"{response_post_delete.Description}");
                    }
                }
                else
                {
                    return View("Error", "Ошибка доступа");
                }
                
            }
            else
            {
                return View("Error", "Ошибка доступа");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> GetPost(int id)
        {
            var response = await _postService.GetPost(id);
            if (response.Status == Domain.Enum.ErrorStatus.Success)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(string TypeCar, string NameCar, int PostDate, int CarDate)
        {
            
            var response = await _postService.GetPosts(TypeCar, NameCar,PostDate,CarDate);

            if (response.Status == ErrorStatus.Success)
            {
                return View(response.Data.ToList());
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<string> names = new List<string>();
                var response = await _carService.GetCars();
                foreach (var name in response.Data)
                {
                    names.Add(name.Name);
                }
                var Data = new PostCreateViewModel()
                {
                    PostViewModel = null,
                    CarNames = names
                };
                if (response.Status == Domain.Enum.ErrorStatus.Success)
                {
                    return View(Data);
                }
                else
                {
                    return View("Error", $"{response.Description}");
                }
            }
            return View("Error", "Ошибка доступа к форме");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, PostCreateViewModel model)
        {
            if(User.Identity.IsAuthenticated)
            {
                if (id == 0)
                {
                    ModelState.Remove("CarNames");
                    ModelState.Remove("PostViewModel.Image");

                    if (ModelState.IsValid)
                    {
                        await _postService.Create(model.PostViewModel, User.Identity.Name);
                        return RedirectToAction("GetPosts", "Post");
                    }
                    else
                    {
                        return View("Error", "Ошибка отправки формы");
                    }
                }
                else
                {
                    ModelState.Remove("CarNames");
                    ModelState.Remove("PostViewModel.Image");
                    if(ModelState.IsValid)
                    {
                        var post = await _postService.GetPostById(id);
                        var user = await _userService.GetUserByName(User.Identity.Name);
                        if (user.Data.Id == post.Data.UserId || User.IsInRole("Admin"))
                        {
                            await _postService.Edit(id, model.PostViewModel);
                            return RedirectToAction("GetPosts", "Post");
                        }
                        else
                        {
                            return View("Error", "Ошибка доступа к форме");
                        }
                    }
                    else
                    {
                        return View("Error", "Ошибка доступа к форме");
                    }
                    
                    
                } 
            }
            else
            {
                return View("Error", "Ошибка доступа к форме");
            }
            
        }
    }
}
