using System;
using System.Collections.Generic;
using SimpleShopORMDemo.Model;

namespace SimpleShopORMDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate ORM object
            ORM_MSSql ORM = new ORM_MSSql("10.142.65.102", "SimpleShop", "SimpleShop", "SimpleShop");

            // List of all products
            //List<Product> products = ORM.GetAllProducts();

            //foreach (Product prod in products) {
            //    Console.WriteLine(prod.ProductName);
            //}

            //// Get a specifik product by ID
            //Product prod = ORM.GetProductById(5);
            //Console.WriteLine(prod.ProductId + ". " + prod.ProductName);

            Product NewProduct = new Product("Passionsfrugt lilla", 11.95m);
            NewProduct = ORM.CreateNewProduct(NewProduct);

            //Console.WriteLine(NewProduct.ProductId);
            //Console.WriteLine(NewProduct.ProductName);
            //Console.WriteLine(NewProduct.ProductPrice);

            //List of all products
            List<Product> products = ORM.GetAllProducts();
            foreach (Product prod in products)
            {
                Console.WriteLine(prod.ProductName);
            }
        }
    }
}
