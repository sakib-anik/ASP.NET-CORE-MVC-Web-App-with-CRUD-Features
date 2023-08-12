using DesignPart.Data;
using DesignPart.Models.Account;
using DesignPart.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DesignPart.Controllers.Account
{
    public class AccountController : Controller
    {
        private EFCoreDbContext context;
        public AccountController(EFCoreDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Users.Where(e => e.Email == model.Email).SingleOrDefault();
                if(data != null)
                {
                    bool isValid = (data.Email == model.Email && data.Password == model.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", model.Email);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid password!";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUsername"] = "Username not found!";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        

        [AcceptVerbs("Post","Get")]
        public IActionResult UserNameIsExist(string userName)
        {
            var data = context.Users.Where(e => e.Email == userName).SingleOrDefault();
            if (data != null)
            {
                return Json($"Username {userName} already in use!");
            }
            else
            {
                return Json(true);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile,
                    IsActive = model.IsActive
                };
                context.Users.Add(data);
                context.SaveChanges();
                TempData["successMessage"] = "You are eligible to login, please fill own credentials then login!";
                return RedirectToAction("Login");
            }
            else {
                TempData["errorMessage"] = "Empty form can't be submitted!";
                return View(model);
            }
        }
    }
}
