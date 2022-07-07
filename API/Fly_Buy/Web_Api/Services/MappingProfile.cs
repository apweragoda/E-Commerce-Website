using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, Product>()
                .ForMember(p => p.Id, ex => ex.MapFrom(p => p.Id)).ReverseMap();
        }
    }
}
