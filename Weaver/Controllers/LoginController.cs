using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service;
using Weaver.Models;
using Common.Utility;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weaver.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.CheckUser(model.UserName, model.Password);
                if(user != null)
                {
                    HttpContext.Session.Set("CurrentUser", ByteConvertHelper.ObjectToBytes(user));
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrorInfo = "User Name or Password Incorrect";
                return View();
            }
            ViewBag.ErrorInfo = ModelState.Values.Where(e=>e.ValidationState == ModelValidationState.Invalid).First().Errors[0].ErrorMessage;
            return View(model);
        }
    }
}
