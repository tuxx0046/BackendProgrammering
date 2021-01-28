using System;
namespace SimpleShopORMDemo.Model
{
    public class Product
    {
        // Properties
        public int ProductId { get; set; }
        public string ProductName { get; }
        public decimal ProductPrice { get; set; }

        // Constructor, takes 2 params
        public Product(string name, decimal price )
        {
            ProductName = name;
            ProductPrice = price;
        }

        // Constructor overload, takes 3 params
        public Product(int id, string name, decimal price)
        {
            ProductId = id;
            ProductName = name;
            ProductPrice = price;
        }
    }
}
