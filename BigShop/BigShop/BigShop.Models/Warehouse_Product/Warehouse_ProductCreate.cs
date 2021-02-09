using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Warehouse_Product
{
    public class Warehouse_ProductCreate
    {
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int Warehouse_Id { get; set; }
    }
}
