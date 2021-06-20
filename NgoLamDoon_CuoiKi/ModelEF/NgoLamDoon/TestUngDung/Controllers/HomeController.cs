using ModelEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;

namespace TestUngDung.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string keysearch, int page = 1, int pagesize = 9)
        {
            var users = new productDAO();
            var model = users.ListWhereAll(keysearch, page, pagesize);
            return View(model);
        }
        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }

    }
}