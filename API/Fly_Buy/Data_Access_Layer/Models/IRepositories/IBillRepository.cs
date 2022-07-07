using Data_Access_Layer.Repository.Entities;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface IBillRepository
    {
        IEnumerable<Bill> GetAllBillDetails();
        Bill AddBillDetails(Bill bill);
    }
}