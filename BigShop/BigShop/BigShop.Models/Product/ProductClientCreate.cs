using BigShop.Models.Warehouse_Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.Product
{
    public class ProductClientCreate
    {
        public ProductCreate Product { get; set; }
        public Warehouse_ProductClientCreate QuantityAndWarehouse { get; set; }
    }
}
