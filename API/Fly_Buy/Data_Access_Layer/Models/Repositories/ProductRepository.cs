using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Access_Layer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FlyBuyDbContext _ctx;

        public ProductRepository(FlyBuyDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                .ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public Product AddProduct(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            if (product == null)
                return null;

            _ctx.Products.Update(product);
            _ctx.SaveChanges();            

            return product;
        }

        public int DeleteProduct(int id)
        {
            var product = _ctx.Products
                .FirstOrDefault(p => p.Id == id);

            _ctx.Products.Remove(product);
            return _ctx.SaveChanges();
        }
    }
}
