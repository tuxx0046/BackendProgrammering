using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.OrderLine
{
    public class OrderLineCreate : OrderLineClientCreate
    {
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 9999999.99)]
        public decimal Price { get; set; }
        [Required]
        public int CustomerOrder_Id { get; set; }
    }
}
