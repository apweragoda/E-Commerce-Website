using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class OrderCreationModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public ICollection<OrderItemCreationModel> Items { get; set; }
        [Required]
        public ShippingCreationModel Shipping { get; set; }
        [Required]
        public BillCreationModel Bill { get; set; }
    }
}
