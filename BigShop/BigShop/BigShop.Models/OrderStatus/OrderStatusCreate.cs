using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.OrderStatus
{
    public class OrderStatusCreate
    {
        [Required(ErrorMessage = "Status name is required")]
        [MinLength(5, ErrorMessage = "Must be between 1-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 1-50 characters")]
        public string Name { get; set; }
    }
}