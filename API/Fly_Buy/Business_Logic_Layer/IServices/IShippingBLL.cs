using Business_Logic_Layer.Models;
using System.Collections.Generic;

namespace Business_Logic_Layer.Services
{
    public interface IShippingBLL
    {
        IEnumerable<ShippingModel> GetAllShippingDetails();
        ShippingModel AddShippingDetails(ShippingCreationModel shipping);
    }
}