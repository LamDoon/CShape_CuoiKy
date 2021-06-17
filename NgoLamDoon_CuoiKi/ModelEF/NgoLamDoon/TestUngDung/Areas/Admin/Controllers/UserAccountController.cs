using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserAccountController : BaseController
    {
        NgoLamDoonContext dbModel = new NgoLamDoonContext();
        // GET: Admin/UserAccount
        public ActionResult Index(string keysearch, int page=1, int pagesize=5)
        {
            var users = new userDAO();
            var model = users.ListWhereAll(keysearch, page,pagesize);
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            try
            {
                UserAccount users = dbModel.UserAccounts.Where(val => val.ID == id).Single<UserAccount>();
                if (users.Status == "Blocked")
                {
                    users.Status = "Active";
                    SetAlert("Kích hoạt tài khoản thành công", "success");
                }
                else
                {
                    users.Status = "Blocked";
                    SetAlert("Đã vô hiệu hoá tài khoản", "warning");
                }
                dbModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }

        }

        public ActionResult Delete(string id)
        {
            try
            {
                UserAccount users = dbModel.UserAccounts.Where(val => val.ID == id).Single<UserAccount>();
                dbModel.UserAccounts.Remove(users);
                dbModel.SaveChanges();
                // TODO: Add delete logic here
                SetAlert("Xoá tài khoản thành công", "success");
                return RedirectToAction("Index");


            }
            catch
            {

                return View();
            }

        }

    }
}