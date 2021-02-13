using BigShop.Models.OrderLine;
using BigShop.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.CustomerOrder
{
    public class CustomerOrderClientCreate
    {
        [Required]
        public int Customer_Id { get; set; }
        [Required]
        public int Courier_Id { get; set; }
        [Required]
        public int PaymentMethod_Id { get; set; }
        public List<OrderLineClientCreate> OrderLines { get; set; }
    }
}
