using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Net;

namespace MVC5Course.Controllers
{
    public class TestController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: Test
        public ActionResult Index()
        {
            var data = from p in db.Product
                       select p;
            return View(data.Take(10));
        }
        public ActionResult Create()
        {
            return View();
        }
        //接收Post資料
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Product data)
        {
            //經過驗正資料
            if (ModelState.IsValid)
            {
                db.Product.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public ActionResult Edit(int? id)
        {
            //找到id
            var item = db.Product.Find(id);
            return View(item);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product data)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);

                item.ProductName = data.ProductName;
                item.Price = data.Price;
                item.Stock = data.Stock;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(data);
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        //狀態回傳訊息 HttpStatusCodeResult
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var product = db.Product.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}


        ////取得post資料，在做刪除
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var product = db.Product.Find(id);
        //    db.Product.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");



        //取得post資料，在做刪除
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var item = db.Product.Find(id);
            db.OrderLine.RemoveRange(item.OrderLine.ToList());
            db.Product.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Details(int? id)
        {
            var product = db.Product.Find(id);
            return View(product);
        }
    }
}