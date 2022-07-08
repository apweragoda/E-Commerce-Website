using Data_Access_Layer.Repository.Entities;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        int DeleteProduct(int id);
        ICollection<Product> GetAllProducts();
        Product GetSingleProduct(int id);
        ICollection<Product> GetProductsByCategory(string category);
        
    }
}