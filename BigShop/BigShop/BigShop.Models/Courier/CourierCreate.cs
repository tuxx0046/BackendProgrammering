using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Courier
{
    public class CourierCreate
    {
        [Required(ErrorMessage = "Courier name is required")]
        [MinLength(3, ErrorMessage = "Must be between 3-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 3-50 characters")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Initial cost is required")]
        [Range(0, 9999999.99)]
        public decimal InitialCost { get; set; }
        [Required(ErrorMessage = "Weight fee is required")]
        [Range(0, 9999999.99)]
        public decimal WeightFee { get; set; }
    }
}
