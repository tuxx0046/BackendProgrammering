using Microsoft.Data.SqlClient;
using SimpleORMDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleORMDemo.DataAccess
{
    public class CategoryORM : ICategoryORM
    {
        private readonly SqlConnection _connection;

        public CategoryORM()
        {
            _connection = MSSQLConnection.GetConnection();
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT Id, Name FROM dbo.Category";
            SqlCommand cmd = new SqlCommand(query, _connection);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }


            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                categories.Add(new Category(reader.GetInt32(0), reader.GetString(1)));
            }

            //_connection.Close();
            reader.Close();

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category cat = null;
            string query = "SELECT Id, Name FROM dbo.Category WHERE id = @value";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@value", id);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                cat = new Category(reader.GetInt32(0), reader.GetString(1));
            }

            if (cat == null)
            {
                //throw new IndexOutOfRangeException("Category with that ID doesn't exist.");
                Console.WriteLine("NO category with this ID exists");
            }

            //_connection.Close();
            reader.Close();

            return cat;
        }
    }
}
