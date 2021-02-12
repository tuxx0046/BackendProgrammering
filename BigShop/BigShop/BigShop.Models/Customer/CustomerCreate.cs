using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Customer
{
    public class CustomerCreate
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 1-50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 1-100 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid phone number")]
        [MinLength(7, ErrorMessage = "Must be between 7-50 numbers")]
        [MaxLength(50, ErrorMessage = "Must be between 7-50 numbers")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 1-100 characters")]
        public string AddressLane { get; set; }
        [Required(ErrorMessage = "Zip ID is required")]
        public int Zip_Id { get; set; }
        //[Required(ErrorMessage = "Application User ID is required")]
        //public int ApplicationUser_Id { get; set; }
    }
}
