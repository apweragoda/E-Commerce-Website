using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class BillCreationModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Invoice { get { return "INVOICE NO - " + Id; } }
        [Required]
        public float Total { get; set; }
        [Required]
        public float Discount { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public DateTime PayementDate { get; set; }
    }
}
