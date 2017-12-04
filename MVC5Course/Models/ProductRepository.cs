using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> Get取得所有尚未刪除的商品資料()
        {
            return this.All().Where(p => p.IsDeleted == false);
        }
        public IQueryable<Product> Get取得所有尚未刪除的商品資料Top10()
        {
            return this.All().Where(p => p.IsDeleted == false).Take(10);
        }

        public override void Delete(Product entity)
        {
            entity.IsDeleted = true;
            this.UnitOfWork.Commit();
        }
    }

    /// <summary>
    /// 建構子
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {

    }

}