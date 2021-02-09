using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.PaymentMethod
{
    public class PaymentMethodCreate
    {
        [Required(ErrorMessage = "Payment method name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 1-50 characters")]
        public string Name { get; set; }
    }
}