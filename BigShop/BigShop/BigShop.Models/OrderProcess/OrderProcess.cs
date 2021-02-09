using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.OrderProcess
{
    public class OrderProcess : OrderProcessCreate
    {
        public DateTime ProcessDate { get; set; }
    }
}