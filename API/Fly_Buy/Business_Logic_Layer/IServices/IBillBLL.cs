using Business_Logic_Layer.Models;
using System.Collections.Generic;

namespace Business_Logic_Layer.Services
{
    public interface IBillBLL
    {
        IEnumerable<BillModel> GetAllBillDetails();
        BillModel AddBillDetails(BillCreationModel bill);
    }
}