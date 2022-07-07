using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Services
{
    public class ShippingBLL : IShippingBLL
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IMapper _shippingMapper;

        public ShippingBLL(IShippingRepository shippingRepository, IMapper shippingMapper)
        {
            _shippingRepository = shippingRepository;
            _shippingMapper = shippingMapper;
        }
        public IEnumerable<ShippingModel> GetAllShippingDetails()
        {
            var shipping = _shippingRepository.GetAllShippingDetails();
            var shippingModel = _shippingMapper.Map<IEnumerable<ShippingModel>>(shipping);
            return shippingModel;
        }

        public ShippingModel AddShippingDetails(ShippingCreationModel shipping)
        {
            if (shipping == null)
                return null;

            var shippingDetails = _shippingMapper.Map<Shipping>(shipping);
            var createdShippingDetails = _shippingRepository.AddShippingDetails(shippingDetails);
            var createdShippingDetailsModel = _shippingMapper.Map<ShippingModel>(createdShippingDetails);

            return createdShippingDetailsModel;
        }
    }
}
