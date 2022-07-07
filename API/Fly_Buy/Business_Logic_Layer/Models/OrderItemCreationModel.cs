using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class OrderItemCreationModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ProductModel Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public OrderModel Order { get; set; }
    }
}
