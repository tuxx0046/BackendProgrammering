using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.OrderLine
{
    public class OrderLineClientCreate
    {
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required]
        public int Product_Id { get; set; }
    }
}
