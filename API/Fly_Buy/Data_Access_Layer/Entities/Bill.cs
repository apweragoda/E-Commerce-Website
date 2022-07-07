using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repository.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public string Invoice { get { return "INVOICE NO - " + Id; } }
        public float Total { get; set; }
        public float Discount { get; set; }
        public string PaymentType { get; set; }
        public DateTime PayementDate { get; set; }
    }
}
