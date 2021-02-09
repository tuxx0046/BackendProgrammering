using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Country
{
    public class CountryCreate
    {
        [Required(ErrorMessage = "Country name is required")]
        [MinLength(2, ErrorMessage = "Must be between 2-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 2-100 characters")]
        public string Name { get; set; }
    }
}
