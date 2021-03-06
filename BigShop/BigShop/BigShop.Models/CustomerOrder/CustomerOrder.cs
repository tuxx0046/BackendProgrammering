﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigShop.Models.CustomerOrder
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal InitialShippingCost { get; set; }
        public decimal WeightFee { get; set; }
        public int Customer_Id { get; set; }
        public int Courier_Id { get; set; }
        public int PaymentMethod_Id { get; set; }
    }
}