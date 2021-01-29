using Microsoft.Data.SqlClient;
using SimpleORMDemo.DataAccess;
using SimpleORMDemo.Models;
using System;
using System.Collections.Generic;

namespace SimpleORMDemo
{
    class Program
    {
        static void Main()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            ProductORM productORM = new ProductORM();

            /////////// Fetch data ///////////////
            /*
            List<Product> products = productORM.GetAllProducts();
            foreach (Product product in products)
            {
                Console.WriteLine($"Id: {product.Id.ToString()}, " +
                    $"{product.Name}, " +
                    $"{product.Price.ToString()}, " +
                    $"{product.WeightGram}, " +
                    $"{product.Manufacturer.Name}, " +
                    $"{product.Category.Name}");
            }

            Product prod = productORM.GetProductById(5);
            Console.WriteLine($"Id: {prod.Id.ToString()}, " +
                    $"{prod.Name}, " +
                    $"{prod.Price.ToString()}, " +
                    $"{prod.WeightGram}, " +
                    $"{prod.Manufacturer.Name}, " +
                    $"{prod.Category.Name}");
            */

            ////////////// Insert + Update data ///////////
            /*
            ManufacturerORM manuConnect = new ManufacturerORM();
            List<Manufacturer> manufacturers = manuConnect.GetAllManufacturers();
            CategoryORM catConnect = new CategoryORM();
            List<Category> categories = catConnect.GetAllCategories();

            Product NewProduct = new Product("Electrolux TV", 17777.77m, "1235468956321", 5523, manufacturers[5], categories[0]);
            Product ReturnedProduct = productORM.UpsertProduct(NewProduct);
            Console.WriteLine(ReturnedProduct.Name + " " + ReturnedProduct.Price.ToString());

            Product UpdateProduct = new Product(ReturnedProduct.Id, "Updated TV", 100.99m, "55468", 5523, manufacturers[5], categories[0]);
            UpdateProduct = productORM.UpsertProduct(UpdateProduct, UpdateProduct.Id);
            Console.WriteLine(UpdateProduct.Id + " " + UpdateProduct.Name + " " + UpdateProduct.Price.ToString());
            */

            /////////////// Delete data /////////////
            productORM.RemoveProductById(27);

            //0('Philips')
            //1('Sony') 
            //2('Samsung')
            //3('Dell')
            //4'Lenovo')
            //5('Electrolux')
            //6('Nikon')
            //7('Sandberg')
            //8('Dyson')

            //0('TV'),
            //1('Mobiltelefon'),
            //2('Tablet'),
            //3(N'Stationær PC'),
            //4('Laptop'),
            //5('Fryser'),
            //6('Kamera'),
            //7('Hukommelseskort'),
            //8(N'Støvsuger')


            /*
            CategoryORM catConnect = new CategoryORM();
            List<Category> categories = catConnect.GetAllCategories();

            foreach (var item in categories)
            {
                Console.WriteLine(item.Name);
            }

            Category category = catConnect.GetCategoryById(2);
            Console.WriteLine(category.Name);

            ManufacturerORM manuConnect = new ManufacturerORM();
            List<Manufacturer> manufacturers = manuConnect.GetAllManufacturers();
            foreach (var item in manufacturers)
            {
                Console.WriteLine(item.Name);
            }

            Manufacturer manu = manuConnect.GetManufacturerById(1);
            Console.WriteLine(manu.Name);

            Console.WriteLine("日本語"); 
            */

        }
    }
}
