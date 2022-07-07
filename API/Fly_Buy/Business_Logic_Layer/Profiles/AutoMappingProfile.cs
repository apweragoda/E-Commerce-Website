using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Profiles
{
    public class AutoMappingProfile : AutoMapper.Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Product, ProductCreationModel>().ReverseMap();
            CreateMap<Product, OrderItemModel>().ReverseMap();
            CreateMap<ProductModel, OrderItemModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserCreationModel>().ReverseMap();
            CreateMap<User, LoginModel>().ReverseMap();
            CreateMap<UserModel, LoginModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Order, OrderCreationModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemCreationModel>().ReverseMap();
            CreateMap<Shipping, ShippingModel>().ReverseMap();
            CreateMap<Shipping, ShippingCreationModel>().ReverseMap();
            CreateMap<Bill, BillModel>().ReverseMap();
            CreateMap<Bill, BillCreationModel>().ReverseMap();
        }
    }
}
