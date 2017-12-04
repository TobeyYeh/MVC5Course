using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Net;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {
        FabricsEntities db = new FabricsEntities();

        // GET: Test
        //public ActionResult Index()
        //{
        //    var data = from p in db.Product
        //               select p;
        //    return View(data.Take(10));
        //}
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

                repo.UnitOfWork.Commit();

                TempData["ProductItem"] = item;


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

        //public ActionResult Index()
        //{
        //    var data = from p in db.Product
        //               where p.IsDeleted == false
        //               select p;
        //    return View(data.Take(10));
        //}

        public ActionResult Index()
        {
            var data = repo.All().Where(p => p.IsDeleted == false);
            //var data = repo.Get取得所有尚未刪除的商品資料Top10();

            return View(data.Take(10));
            //return View(data);
        }


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


        public ActionResult Delete(int id)
        {
            //being 只做一次交易
            var olRepo = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

            //var olRepo = new OrderLineRepository();
            //olRepo.UnitOfWork = repo.UnitOfWork;
            //olRepo.Delete(olRepo.All().First(p => p.OrderId == 1));

            var item = repo.Find(id);
            repo.Delete(item);

            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

    }
}

