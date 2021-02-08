using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Account
{
    public class ApplicationUserCreate : ApplicationUserLogin
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(30, ErrorMessage = "Must be at most 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
