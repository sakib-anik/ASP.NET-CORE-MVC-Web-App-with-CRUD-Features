using Microsoft.AspNetCore.Mvc;
using DesignPart.Data;
using DesignPart.Models;
using System.Collections.Generic;
using Microsoft.DotNet.MSIdentity.Shared;
using DesignPart.Models.Account;

namespace PartB.Controllers
{
    public class UserController : Controller
    {
        private EFCoreDbContext context;
        public UserController(EFCoreDbContext _context)
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

        [HttpGet]
        public IActionResult GetUserFormAndList()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult GetUserList() {
            try
            {
                var list = context.Users.ToList();
                //var list = new List<User>() { new User() { Uid = 1, Name = "Sakib", Contact = "+8801780943003", Email = "sakib.within@gmail.com" }, new User() { Uid = 2, Name = "Rakib", Contact = "+8801580943553", Email = "rakib.within@gmail.com" }, new User() { Uid = 3, Name = "Anik", Contact = "+8801980943003", Email = "anik.within@gmail.com" }, new User() { Uid = 4, Name = "Anika", Contact = "+8801580743003", Email = "anika.within@gmail.com" } };
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = true, Data = list });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = false, Message = msg });
            }
        }

        [HttpPost]
        public IActionResult SaveUser(User model)
        {
            try
            {
                if(model.Id > 0) // edit
                {
                    context.Users.Update(model);
                    context.SaveChanges();
                }
                else // insert
                {
                    context.Users.Add(model);
                    context.SaveChanges();
                }
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = true, Data = model, Message = "Records saved Successfully." });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = false, Message = msg });
            }
        }

        [HttpGet]
        public IActionResult Edit(int uid) {
            try
            {
                var obj = context.Users.Find(uid);
                return Json(new DesignPart.Models.JsonResponse() { Data = obj, IsSuccess = true });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = false, Message = msg });
            }
        }

        [HttpPost]
        public IActionResult Delete(int uid)
        {
            try
            {
                var obj = context.Users.Find(uid);
                context.Users.Remove(obj);
                context.SaveChanges();
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = true, Message = "Record deleted successfully." });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new DesignPart.Models.JsonResponse() { IsSuccess = false, Message = msg });
            }
        }
    }
}
