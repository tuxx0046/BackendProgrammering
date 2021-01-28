using SimpleORMDemo.Models;
using System.Collections.Generic;

namespace SimpleORMDemo.DataAccess
{
    public interface IProductORM
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void RemoveProductById(int id);
        Product UpsertProduct(Product product, int? id = null);
    }
}