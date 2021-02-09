using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Product
{
    public class ProductCreate
    {
        [Required(ErrorMessage = "Product name is required")]
        [MinLength(5, ErrorMessage = "Must be between 5-150 characters")]
        [MaxLength(150, ErrorMessage = "Must be between 5-150 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 9999999.99)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "EAN is required")]
        [MinLength(13)]
        [MaxLength(13)]
        public string EAN { get; set; }
        [Required(ErrorMessage = "Product weight is required")]
        [Range(0, 9999999)]
        public int WeightGram { get; set; }
        [Required]
        public int Manufacturer_Id { get; set; }
        [Required]
        public int Category_Id { get; set; }
    }
}