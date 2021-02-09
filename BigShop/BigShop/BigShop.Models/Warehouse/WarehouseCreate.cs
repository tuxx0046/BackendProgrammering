using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Warehouse
{
    public class WarehouseCreate
    {
        [Required(ErrorMessage = "Warehouse name is required")]
        [MinLength(5, ErrorMessage = "Must be between 5-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 5-50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 1-100 characters")]
        public string AddressLane { get; set; }
        [Required]
        public int Zip_Id { get; set; }
    }
}