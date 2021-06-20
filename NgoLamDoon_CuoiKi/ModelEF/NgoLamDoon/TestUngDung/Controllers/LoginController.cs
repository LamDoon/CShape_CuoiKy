using ModelEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;
using TestUngDung.Models;

namespace TestUngDung.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var dao = new adminDAO();
                Session["USER_SESSION"] = "";
                var result = dao.login(user.UserName, Encryptor.EncryptMD5(user.Password));
                if (result == 1)
                {
                    //  ModelState.AddModelError("", "Đăng nhập thành công");
                    Session.Add(Constants.USER_SESSION, user.UserName);
                    Session["USER_SESSION"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
                if (Session["USER_SESSION"] == null) return RedirectToAction("Index", "Login");
            }
            return View("Index");
        }
    }
}
