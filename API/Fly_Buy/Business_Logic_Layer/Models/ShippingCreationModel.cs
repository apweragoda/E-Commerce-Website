using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_Logic_Layer.Models
{
    public class ShippingCreationModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public float Fee { get; set; }
    }
}
