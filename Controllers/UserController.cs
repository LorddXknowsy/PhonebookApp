using ContactApp.Models;
using ContactApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows;

namespace ContactApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register(User user)
        {

            var userRepository = new UserRepository();
            userRepository.SaveUser(user);
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string UserName, string Password)
        {
            bool isAuth = false;
            var userRepository = new UserRepository();
            var authUser = userRepository.GetUser(UserName, Password);
            if (authUser.UserName != null)
            {
                Session["AuthUser"] = authUser;
                isAuth = true;
                
            }
            else
            {
               
            }
            return Json(isAuth);
        }
    }
}