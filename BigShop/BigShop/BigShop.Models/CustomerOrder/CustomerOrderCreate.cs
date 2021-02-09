using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.CustomerOrder
{
    public class CustomerOrderCreate
    {
        [Required(ErrorMessage = "Initial cost is required")]
        [Range(0, 9999999.99)]
        public decimal InitialShippingCost { get; set; }
        [Required(ErrorMessage = "Weight fee is required")]
        [Range(0, 9999999.99)] 
        public decimal WeightFee { get; set; }
        [Required]
        public int Customer_Id { get; set; }
        [Required]
        public int Courier_Id { get; set; }
        [Required]
        public int PaymentMethod_Id { get; set; }
    }
}
