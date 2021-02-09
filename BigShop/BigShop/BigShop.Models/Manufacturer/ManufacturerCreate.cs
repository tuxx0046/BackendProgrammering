using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Manufacturer
{
    public class ManufacturerCreate
    {
        [Required(ErrorMessage = "Manufacturer name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 1-100 characters")]
        public string Name { get; set; }
    }
}
