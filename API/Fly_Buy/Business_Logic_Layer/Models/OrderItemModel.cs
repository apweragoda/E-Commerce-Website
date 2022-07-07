using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public OrderModel Order { get; set; }
    }
}
