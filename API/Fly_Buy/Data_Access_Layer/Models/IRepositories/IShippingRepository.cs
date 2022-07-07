using Data_Access_Layer.Repository.Entities;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface IShippingRepository
    {
        IEnumerable<Shipping> GetAllShippingDetails();
        Shipping AddShippingDetails(Shipping shipping);
    }
}