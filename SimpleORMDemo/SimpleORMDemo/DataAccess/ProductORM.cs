using Microsoft.Data.SqlClient;
using SimpleORMDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleORMDemo.DataAccess
{
    public class ProductORM : IProductORM
    {
        private readonly SqlConnection _connection;
        private readonly ManufacturerORM _manufacturerORM;
        private readonly CategoryORM _categoryORM;

        public ProductORM()
        {
            _connection = MSSQLConnection.GetConnection();
            _manufacturerORM = new ManufacturerORM();
            _categoryORM = new CategoryORM();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT Id, Name, Price, EAN, WeightGram, Manufacturer_Id, Category_Id FROM dbo.Product";
            SqlCommand cmd = new SqlCommand(query, _connection);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                products.Add(new Product(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetDecimal(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    _manufacturerORM.GetManufacturerById(reader.GetInt32(5)),
                    _categoryORM.GetCategoryById(reader.GetInt32(6))
                    ));
            }

            _connection.Close();

            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            string query = "SELECT Id, Name, Price, EAN, WeightGram, Manufacturer_Id, Category_Id FROM dbo.Product WHERE id = @value";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@value", id);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                product = new Product(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetDecimal(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    _manufacturerORM.GetManufacturerById(reader.GetInt32(5)),
                    _categoryORM.GetCategoryById(reader.GetInt32(6))
                    );
            }

            if (product == null)
            {
                //throw new IndexOutOfRangeException("Category with that ID doesn't exist.");
                Console.WriteLine("NO Product with this ID exists");
            }

            _connection.Close();

            return product;
        }

        public Product UpsertProduct(Product product, int? id = null)
        {
            string query;
            // If inserting
            if (id == null)
            {
                query = "INSERT INTO dbo.Product(Name, Price, EAN, WeightGram, Manufacturer_Id, Category_Id) " +
                    "OUTPUT INSERTED.ID " +
                    "VALUES (@name, @price, @ean, @weightGram, @manufacturer_id, @category_id); ";
            }
            // If updating
            else
            {
                query = "UPDATE dbo.Product " +
                        "SET " +
                        "Name = @name," +
                        "Price = @price," +
                        "EAN = @ean," +
                        "WeightGram = @weightGram," +
                        "Manufacturer_Id = @manufacturer_id," +
                        "Category_Id = @category_id " +
                        "WHERE Id = @id; " +
                        "SELECT @id;";
            }
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@ean", product.EAN);
            cmd.Parameters.AddWithValue("@weightGram", product.WeightGram);
            cmd.Parameters.AddWithValue("@manufacturer_id", product.Manufacturer.Id);
            cmd.Parameters.AddWithValue("@category_id", product.Category.Id);

            // If updating add following
            if (id != null)
            {
                cmd.Parameters.AddWithValue("@id", id);
            }

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            // ExecuteScalar returns first first column of first row of result set
            product.Id = (int)cmd.ExecuteScalar();

            _connection.Close();

            return product;
        }

        public void RemoveProductById(int id)
        {
            string query = "DELETE FROM dbo.Product WHERE id = @value";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@value", id);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            int rowsAffected = cmd.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
