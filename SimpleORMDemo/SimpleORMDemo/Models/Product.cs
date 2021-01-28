using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleORMDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string EAN { get; set; }
        public int WeightGram { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Category Category { get; set; }

        public Product(string name, decimal price, string ean, int weightGram, Manufacturer manu, Category cat)
        {
            Name = name;
            Price = price;
            EAN = ean;
            WeightGram = weightGram;
            Manufacturer = manu;
            Category = cat;
        }

        public Product(int id, string name, decimal price, string ean, int weightGram, Manufacturer manu, Category cat)
        {
            Id = id;
            Name = name;
            Price = price;
            EAN = ean;
            WeightGram = weightGram;
            Manufacturer = manu;
            Category = cat;
        }
    }
}
