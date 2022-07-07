using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;
using System.Collections.Generic;

namespace Business_Logic_Layer
{
    public interface IProductBLL
    {
        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<ProductModel> GetProductsByCategory(string category);
        ProductModel AddProduct(ProductCreationModel product);
        ProductModel UpdateProduct(ProductCreationModel product);
        int DeleteProduct(int id);
    }
}