using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Access_Layer.Repository
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly FlyBuyDbContext _ctx;

        public ShippingRepository(FlyBuyDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Shipping> GetAllShippingDetails()
        {
            return _ctx.ShippingInfo.ToList();
        }

        public Shipping AddShippingDetails(Shipping shipping)
        {
            _ctx.ShippingInfo.Add(shipping);
            _ctx.SaveChanges();
            return shipping;
        }
    }
}
