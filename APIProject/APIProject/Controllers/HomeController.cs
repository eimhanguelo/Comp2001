using APIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly APIDBContext context;

        public HomeController(APIDBContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var myUser = context.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (myUser != null)
            {
                HttpContext.Session.SetString("EmailSession", myUser.Email);
                HttpContext.Session.SetString("PasswordSession", myUser.Password);
                HttpContext.Session.SetString("RoleSession", myUser.Role); // Storing user role in session
                HttpContext.Session.SetString("UserIdSession", myUser.UserId.ToString()); // Convert UserId to string
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Login Failed! Please try again";
                return View();
            }
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                ViewBag.EmailSession = HttpContext.Session.GetString("EmailSession").ToString();
                ViewBag.PasswordSession = HttpContext.Session.GetString("PasswordSession").ToString();
                ViewBag.RoleSession = HttpContext.Session.GetString("RoleSession").ToString();
                ViewBag.UserIdSession = HttpContext.Session.GetString("UserIdSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [AllowAnonymous]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                HttpContext.Session.Remove("EmailSession");
                HttpContext.Session.Remove("PasswordSession");
                HttpContext.Session.Remove("RoleSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}