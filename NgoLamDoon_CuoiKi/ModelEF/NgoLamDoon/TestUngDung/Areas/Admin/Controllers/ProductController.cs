using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        NgoLamDoonContext dbModel = new NgoLamDoonContext();
        // GET: Admin/Product
        public ActionResult Index(string keysearch, int page = 1, int pagesize = 10)
        {           
            var users = new productDAO();
            var model = users.ListWhereAll(keysearch, page, pagesize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        public bool checkName(string name, int? type)
        {
            var prd = from s in dbModel.Products select s;
            foreach (var i in prd)
            {
                if (String.IsNullOrEmpty(name) || (i.Name ==name && i.IDType == type))
                {
                    return false;
                }                
            }
            return true;
        }

        public bool IsDouble(string text)
        {
            Double num = 0;
            bool isDouble = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDouble = Double.TryParse(text, out num);

            return isDouble;
        }

        public bool IsInt(int? quantity)
        {
           
            bool isInt = true;

            // Check for empty string.
            if (quantity < 0)
            {
                return false;
            }            

            return isInt;
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase uploadhinh)
        {
            try
            {
                Product prd = new Product();
                if (checkName(model.Name,model.IDType) == false ) 
                {
                    SetAlert("Sản phẩm đã có trong danh sách!", "warning");                    
                    return RedirectToAction("Create");
                }
                else
                    if (IsDouble(model.UnitCost.ToString()) == false)
                {
                    SetAlert("Đơn giá nhập sai dữ liệu. Vui lòng nhập lại!", "warning");
                    return RedirectToAction("Create");
                }
                else
                    if (IsInt(model.Quantity) == false)
                {
                    SetAlert("Số lượng còn nhập sai dữ liệu. Vui lòng nhập lại!", "warning");
                    return RedirectToAction("Create");
                }
                else
                {
                    dbModel.Products.Add(model);
                    dbModel.SaveChanges();

                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = int.Parse(dbModel.Products.ToList().Last().ID.ToString());
                        string _FileName = "";
                        int index = uploadhinh.FileName.IndexOf('.');
                        _FileName = "prd" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Upload/Images"), _FileName);
                        uploadhinh.SaveAs(_path);

                        Product iprd = dbModel.Products.FirstOrDefault(x => x.ID == id);
                        iprd.Image = _FileName;
                        dbModel.SaveChanges();

                    }
                    SetAlert("Thêm sản phẩm thành công", "success");
                }
                return RedirectToAction("Index");
            }
            catch
            {                
                ModelState.AddModelError("", "Cập nhật không thành công");
                return View("Index");
            }

        }

        public void SetViewBag(string selectedId = null)
        {
            var typeDAO = new productTypeDAO();
            ViewBag.type = new SelectList(typeDAO.ListAll(), "ID", "Name", selectedId);
           
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                Product prd = dbModel.Products.Where(val => val.ID == id).Single<Product>();
                dbModel.Products.Remove(prd);
                dbModel.SaveChanges();
                // TODO: Add delete logic here
                SetAlert("Xoá sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public ActionResult Details(int? idPrd)
        {
            Product prd = dbModel.Products.Where(val => val.ID == idPrd).Single<Product>();
            SetViewBag();           
            return View(prd);
        }

        // POST: CTSV/Edit/5
        [HttpPost]
        public ActionResult Details(Product model, int? idPrd, HttpPostedFileBase uploadhinh)
        {
            try
            {
                Product prd = dbModel.Products.Single(u => u.ID == model.ID);
               
                    prd.Name = model.Name;
                    prd.UnitCost = model.UnitCost;
                    prd.Quantity = model.Quantity;
                    prd.Descreption = model.Descreption;
                    prd.IDType = model.IDType;

                    dbModel.SaveChanges();

                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        int id = model.ID;
                        string _FileName = "";
                        int index = uploadhinh.FileName.IndexOf('.');
                        _FileName = "prd" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                        string _path = Path.Combine(Server.MapPath("~/Upload/Images"), _FileName);
                        uploadhinh.SaveAs(_path);
                        prd.Image = _FileName;
                        dbModel.SaveChanges();

                    }
                    SetAlert("Cập nhật sản phẩm thành công", "success");
                    return RedirectToAction("Index");
                }
            
            catch
            {
                ModelState.AddModelError("", "Cập nhật không thành công");
                return View("Index");
            }
        }

    }
}