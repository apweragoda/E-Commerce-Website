using Data_Access_Layer.Repository.Entities;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        int DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        
    }
}