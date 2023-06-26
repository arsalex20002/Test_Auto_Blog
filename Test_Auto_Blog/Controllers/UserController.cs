using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Test_Auto_Blog.Domain.ViewModels.User;
using Test_Auto_Blog.Service.Interfaces;
using Test_Auto_Blog.Domain.ViewModels.Post;
using Test_Auto_Blog.Domain.Enum;

namespace Test_Auto_Blog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public UserController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            if (User.Identity.IsAuthenticated)
            {
                var responce = await _postService.GetPostsByName(User.Identity.Name);
                if(responce.Status == ErrorStatus.Success)
                {
                    return View(responce.Data.ToList());
                }
                else
                {
                    return View();
                }

            }
            return View("Error", "Ошибка доступа к форме");
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("Error", "Ошибка доступа к форме");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Login(model);
                if (response.Status == Domain.Enum.ErrorStatus.Success)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("GetPosts", "Post");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View("Error", "Ошибка доступа к форме");

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Register(model);
                if (response.Status == Domain.Enum.ErrorStatus.Success)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("GetPosts", "Post");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GetPosts", "Post");
        }
    }
}
