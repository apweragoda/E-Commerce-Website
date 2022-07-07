using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public UserModel CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemModel> Items { get; set; }
        public ShippingModel Shipping { get; set; }
        public BillModel Bill { get; set; }
    }
}
