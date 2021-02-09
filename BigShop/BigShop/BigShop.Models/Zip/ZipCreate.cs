using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Zip
{
    public class ZipCreate
    {
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "City name is required")]
        [MinLength(1, ErrorMessage = "Must be between 1-100 characters")]
        [MaxLength(100, ErrorMessage = "Must be between 1-100 characters")]
        public string CityName { get; set; }
        [Required]
        public int Country_Id { get; set; }
    }
}