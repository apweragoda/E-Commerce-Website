using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer
{
    public class ProductBLL : IProductBLL
    {
        private readonly ILogger<ProductBLL> logger;
        private readonly IProductRepository productRepository;
        private readonly IMapper productMapper;

        public ProductBLL(ILogger<ProductBLL> logger, IProductRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.productRepository = repository;
            this.productMapper = mapper;
        }
        public ICollection<ProductModel> GetAllProducts()
        {
            logger.LogWarning("Getting all products");
            var product = productRepository.GetAllProducts();
            var result = productMapper.Map<ICollection<ProductModel>>(product);
            return result;
        }

        public ProductModel GetSingleProduct(int id)
        {
            logger.LogWarning("Getting single product - " + id);
            var product = productRepository.GetSingleProduct(id);
            var result = productMapper.Map<ProductModel>(product);
            return result;

        }

        public ICollection<ProductModel> GetProductsByCategory(string category)
        {
            logger.LogWarning("Getting products by category");
            var product = productRepository.GetProductsByCategory(category);
            var result = productMapper.Map<ICollection<ProductModel>>(product);
            return result;
        }

        public ProductModel AddProduct(ProductCreationModel product)
        {
            logger.LogWarning("Adding product - " + product.Name);
            var productEntity = productMapper.Map<Product>(product);
            var result = productRepository.AddProduct(productEntity);
            var productModel = productMapper.Map<ProductModel>(result);
            return productModel;
        }

        public ProductModel UpdateProduct(ProductCreationModel product)
        {
            logger.LogWarning("Updating product - " + product.Name);
            var productEntity = productMapper.Map<Product>(product);
            var result = productRepository.UpdateProduct(productEntity);
            var productModel = productMapper.Map<ProductModel>(result);

            return productModel;
        }

        public int DeleteProduct(int id)
        {
            logger.LogWarning("Deleting product - " + id);
            var result = productRepository.DeleteProduct(id);
            return result;
        }
    }
}
