using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductTypeController : BaseController
    {
        NgoLamDoonContext dbModel = new NgoLamDoonContext();
        // GET: Admin/ProductType
        public ActionResult Index()
        {
            var users = new productTypeDAO();
            var model = users.ListAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {           
            return View("Index");
        }        

        [HttpPost]
        public ActionResult Create(ProductType model)
        {
            try
            {
                // TODO: Add insert logic here
                ProductType type = new ProductType();
                // TODO: Add insert logic here                
                    type.Name = model.Name;
                    type.Description = model.Description;                    

                    dbModel.ProductTypes.Add(type);
                    dbModel.SaveChanges();
                    SetAlert("Thêm loại vải thành công", "success");                
                return RedirectToAction("Index");
            }
            catch
            {                
                ModelState.AddModelError("", "Thêm không thành công");
                return View("Index");
            }

        }

    }
}