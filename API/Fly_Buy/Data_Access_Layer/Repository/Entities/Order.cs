using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Entities
{
    public class Order
  {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        public Shipping Shipping { get; set; }
        public Bill Bill { get; set; }
    }
}
