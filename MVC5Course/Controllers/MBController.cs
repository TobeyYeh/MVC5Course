using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        public class MBBatchUpdateVM
        {
            public int ProductID { get; set; }
            public Nullable<decimal> Price { get; set; }
            public Nullable<bool> Active { get; set; }
            public Nullable<decimal> Stock { get; set; }
        }


        // GET: MB
        public ActionResult Index()
        {
            var data = repo.Get取得所有尚未刪除的商品資料Top10();
            ViewData.Model = data;
            ViewBag.PageTitle = "商品清單";
            return View();
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(MBBatchUpdateVM[] batch)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in batch)
                {
                    var one = repo.Find(item.ProductID);
                    one.Price = item.Price;
                    one.Active = item.Active;
                    one.Stock = item.Stock;
                }
                try
                {
                    repo.UnitOfWork.Commit();
                }
                catch (DbEntityValidationException ex)
                {

                    throw ex;
                }

                return RedirectToAction("Index");
            }

            var data = repo.Get取得所有尚未刪除的商品資料Top10();
            ViewData.Model = data;
            ViewBag.PageTitle = "商品清單";
            return View();
        }
    }
}