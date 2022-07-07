using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Access_Layer.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly FlyBuyDbContext _ctx;

        public BillRepository(FlyBuyDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Bill> GetAllBillDetails()
        {
            return _ctx.Bills.ToList();
        }

        public Bill AddBillDetails(Bill bill)
        {
            _ctx.Bills.Add(bill);
            _ctx.SaveChanges();
            return bill;
        }
    }
}
