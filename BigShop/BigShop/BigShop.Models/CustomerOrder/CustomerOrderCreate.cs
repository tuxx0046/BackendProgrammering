using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.CustomerOrder
{
    public class CustomerOrderCreate : CustomerOrderClientCreate
    {
        [Required]
        [Range(0, 9999999.99)]
        public decimal InitialShippingCost { get; set; }
        [Required]
        [Range(0, 9999999.99)] 
        public decimal WeightFee { get; set; }
    }
}
