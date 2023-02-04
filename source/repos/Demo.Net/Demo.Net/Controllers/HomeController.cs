using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Net.Models;

namespace Demo.Net.Controllers
{
    public class HomeController : Controller
    {
        private DemoAspnetEntities db = new DemoAspnetEntities();
        public ActionResult Index()
        {
            
            return View(db.Users.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult GetValues(string value)
        {
            value = value + " " + DateTime.UtcNow.ToLongTimeString();

            return Json(new { success = true, message = value });
        }

        [HttpPost]
        public ActionResult Create(string userName, string email, string phone)
        {
           if(userName != "" && email!= "" && phone != "")
            {
                User user = new User();
                user.name = userName;
                user.email = email;
                user.phone = phone;
                db.Users.Add(user);
                db.SaveChanges();
                return Json(new { success = true, message = "Thêm thành công" });
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ các trường thông tin" });
            }

            return Json(new { success = false, message = "Thêm không thành công" });
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.id == id);
                return Json(new { success = true, U = user, message = "Lấy thông tin thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Lấy thông tin thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(int id, string userName, string email, string phone)
        {
            try
            {
                if (userName != "" && email != "" && phone != "")
                {
                    var user = db.Users.SingleOrDefault(x => x.id == id);
                    user.name = userName;
                    user.email = email;
                    user.phone = phone;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Vui lòng nhập đầy đủ các trường thông tin" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
               var user = db.Users.SingleOrDefault(x => x.id == id);
               db.Users.Remove(user);
               db.SaveChanges();
               return Json(new { success = true, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new { success = false, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}