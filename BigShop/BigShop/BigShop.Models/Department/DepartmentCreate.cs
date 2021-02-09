using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Department
{
    public class DepartmentCreate
    {
        [Required(ErrorMessage = "Department name is required")]
        [MinLength(5, ErrorMessage = "Must be between 5-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 5-50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid phone number")]
        [MinLength(7, ErrorMessage = "Must be between 7-50 numbers")]
        [MaxLength(50, ErrorMessage = "Must be between 7-50 numbers")]
        public string Phone { get; set; }
        [Required]
        public int Warehouse_Id { get; set; }
    }
}