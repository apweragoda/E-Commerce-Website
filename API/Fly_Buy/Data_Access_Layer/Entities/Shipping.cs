using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repository.Entities
{
    public class Shipping
    {
        public int Id { get; set; }
        public string Province { get; set; }
        public string Town { get; set; }
        public float Fee { get; set; }
    }
}
