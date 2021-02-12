using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Employee
{
    public class EmployeeCreate
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 1-50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 1-100 characters")]
        public string LastName { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid phone number")]
        [MinLength(7, ErrorMessage = "Must be between 7-50 numbers")]
        [MaxLength(50, ErrorMessage = "Must be between 7-50 numbers")]
        public string Phone { get; set; }
        [Required]
        public int Position_Id { get; set; }
        [Required]
        public int Department_Id { get; set; }
        //[Required]
        //public int ApplicationUser_Id { get; set; }
    }
}