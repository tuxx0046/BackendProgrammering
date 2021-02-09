using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.CustomerOrder
{
    public class CustomerOrder : CustomerOrderCreate
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
    }
}