using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer
{
    public class ProductBLL : IProductBLL
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _productMapper;

        public ProductBLL()
        {
           
        }

        public ProductBLL(IProductRepository repository, IMapper mapper)
        {
            _productRepository = repository;
            _productMapper = mapper;
        }
        public IEnumerable<ProductModel> GetAllProducts()
        {
            var product = _productRepository.GetAllProducts();
            var result = _productMapper.Map<IEnumerable<ProductModel>>(product);
            return result;
        }

        public IEnumerable<ProductModel> GetProductsByCategory(string category)
        {
            var product = _productRepository.GetProductsByCategory(category);
            var result = _productMapper.Map<IEnumerable<ProductModel>>(product);
            return result;
        }

        public ProductModel AddProduct(ProductCreationModel product)
        {
            var productEntity = _productMapper.Map<Product>(product);
            var result = _productRepository.AddProduct(productEntity);
            var productModel = _productMapper.Map<ProductModel>(result);
            return productModel;
        }

        public ProductModel UpdateProduct(ProductCreationModel product)
        {
            var productEntity = _productMapper.Map<Product>(product);
            var result = _productRepository.UpdateProduct(productEntity);
            var productModel = _productMapper.Map<ProductModel>(result);

            return productModel;
        }

        public int DeleteProduct(int id)
        {
            var result = _productRepository.DeleteProduct(id);

            return result;
        }
    }
}
