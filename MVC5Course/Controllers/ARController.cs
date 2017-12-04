using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {

            return PartialView("Index");
        }
        public ActionResult ContentTest()
        {
            // 這樣寫會導致維護性降低，不太方便管理!
            return Content("<script>alert('OK');location.href='/';</script>");
        }

        public ActionResult ContentTest_Better()
        {
            return PartialView("JsAlertRedirect", "新增成功");
        }

        public ActionResult FileTest(string dl)
        {
            if (string.IsNullOrEmpty(dl))
            {
                return File(Server.MapPath("~/App_Data/Cat.jpg"), "image/jpeg");
            }
            else
            {
                return File(Server.MapPath("~/App_Data/Cat.jpg"), "image/jpeg", "Cat.jpg");
            }
        }

        public ActionResult JsonTest()
        {
            var data = from p in repo.All()
                       select new
                       {
                           p.ProductId, 
                           p.ProductName,
                           p.Price
                       };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RedirecTest()
        {
            return RedirectToAction("FileTest",new { dl = 1 });
            
        }

        public ActionResult RedirecTest2()
        {
            //return RedirectToAction(new { 
            //Controller = "Home",
            //action = "About",
            //id = 123
            //    });
            
        }
    }
}