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
        private readonly FlyBuyDbContext ctx;

        public ProductRepository(FlyBuyDbContext ctx)
        {
            this.ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public ICollection<Product> GetAllProducts()
        {
            return ctx.Products
                .ToList();
        }

        public ICollection<Product> GetProductsByCategory(string category)
        {
            return ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public Product GetSingleProduct(int id)
        {
            var product = ctx.Products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public Product AddProduct(Product product)
        {
            ctx.Products.Add(product);
            ctx.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            if (product == null)
                return null;

            ctx.Products.Update(product);
            ctx.SaveChanges();            

            return product;
        }

        public int DeleteProduct(int id)
        {
            var product = ctx.Products
                .FirstOrDefault(p => p.Id == id);

            ctx.Products.Remove(product);
            return ctx.SaveChanges();
        }
    }
}
