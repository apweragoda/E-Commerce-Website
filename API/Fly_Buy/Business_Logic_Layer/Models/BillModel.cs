using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class BillModel
    {
        public int Id { get; set; }
        public string Invoice { get { return "INVOICE NO - " + Id; } }
        public float Total { get; set; }
        public float Discount { get; set; }
        public string PaymentType { get; set; }
        public DateTime PayementDate { get; set; }

    }
}
