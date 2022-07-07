using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class ShippingModel
    {
        public int Id { get; set; }
        public string Province { get; set; }
        public string Town { get; set; }
        public float Fee { get; set; }
    }
}
