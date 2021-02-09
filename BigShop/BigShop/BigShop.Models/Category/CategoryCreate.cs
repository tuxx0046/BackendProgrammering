using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Category
{
    public class CategoryCreate
    {
        [Required(ErrorMessage = "Category name is required")]
        [MinLength(5, ErrorMessage = "Must be between 5-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be between 5-50 characters")]
        public string Name { get; set; }
    }
}
