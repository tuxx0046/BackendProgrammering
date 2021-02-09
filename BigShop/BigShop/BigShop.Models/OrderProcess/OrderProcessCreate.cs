using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.OrderProcess
{
    public class OrderProcessCreate
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public int Employee_Id { get; set; }
        [Required(ErrorMessage = "Customer Order ID is required")]
        public int CustomerOrder_Id { get; set; }
        [Required(ErrorMessage = "Order Status ID is required")]
        public int OrderStatus_Id { get; set; }
    }
}