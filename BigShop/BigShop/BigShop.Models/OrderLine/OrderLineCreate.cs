using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.OrderLine
{
    public class OrderLineCreate
    {
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 9999999.99)]
        public decimal Price { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int CustomerOrder_Id { get; set; }
    }
}
