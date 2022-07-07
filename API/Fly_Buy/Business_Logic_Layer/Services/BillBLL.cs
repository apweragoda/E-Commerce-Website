using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Services
{
    public class BillBLL : IBillBLL
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _billMapper;

        public BillBLL(IBillRepository billRepository, IMapper billMapper)
        {
            _billRepository = billRepository;
            _billMapper = billMapper;
        }

        public IEnumerable<BillModel> GetAllBillDetails()
        {
            var bill = _billRepository.GetAllBillDetails();
            var billModel = _billMapper.Map<IEnumerable<BillModel>>(bill);
            return billModel;
        }

        public BillModel AddBillDetails(BillCreationModel bill)
        {
            if (bill == null)
                return null;

            var billDetails = _billMapper.Map<Bill>(bill);
            var createdBillDetails = _billRepository.AddBillDetails(billDetails);
            var createdBillDetailsModel = _billMapper.Map<BillModel>(createdBillDetails);

            return createdBillDetailsModel;
        }
    }
}
