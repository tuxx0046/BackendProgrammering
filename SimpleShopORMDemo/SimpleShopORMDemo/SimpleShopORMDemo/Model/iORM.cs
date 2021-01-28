using System;
using System.Collections.Generic;

namespace SimpleShopORMDemo.Model
{
    public interface iORM
    {

        // Product
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public Product CreateNewProduct(Product product);

    }
}
