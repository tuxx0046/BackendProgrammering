using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleShopORMDemo.Model
{
    public class ORM_MSSql : iORM
    {
        // DB Connection MSSQL
        private SqlConnection dbConn;


        // Constructor, takes DB credentials and host
        public ORM_MSSql(string dbHost, string dbUser, string dbPassword, string dbDatabase)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
            {
                InitialCatalog = dbDatabase,
                UserID = dbUser,
                Password = dbPassword,
                DataSource = dbHost
            };

            // Instianter selve forbindelsen til DB server
            dbConn = new SqlConnection(connectionString.ToString());

            try
            {
                dbConn.Open();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }



        public List<Product> GetAllProducts()
        {
            // Result of query to db
            List<Product> products = new List<Product>();

            string query = "SELECT id, navn, pris FROM produkt";
            SqlCommand cmd = new SqlCommand(query, dbConn);

            // Open connection if closed?
            if(dbConn.State == System.Data.ConnectionState.Closed) dbConn.Open();

            // Send query to DB
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            // ORM - Translate Each datarow to a Product object
            while (reader.Read())
            {
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2)));
            }

            reader.Close();

            return products;
        }

        public Product GetProductById(int id)
        {
            Product resultProduct = null;

            string query = "SELECT id, navn, pris FROM produkt WHERE id = @val";
            SqlCommand cmd = new SqlCommand(query, this.dbConn);
            cmd.Parameters.AddWithValue("@val", id);

            // Open connection if closed?
            if (dbConn.State == System.Data.ConnectionState.Closed) dbConn.Open();

            // Send query to database
            SqlDataReader reader = cmd.ExecuteReader();

            int i = 0;

            while (reader.Read())
            {
                resultProduct = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
                i++;
            }

            // Close connection if open?
            if (dbConn.State == System.Data.ConnectionState.Open) dbConn.Close();

            if (i != 1) // NOT one product datarow
            {
                throw new IndexOutOfRangeException("Produkt uspecifik fejl!");
            }
            else {
                return resultProduct;
            }
        }

        public Product CreateNewProduct(Product product)
        {
            string query = "INSERT INTO produkt (navn, pris) output INSERTED.ID VALUES (@name, @price)";
            SqlCommand cmd = new SqlCommand(query, dbConn);
            cmd.Parameters.AddWithValue("@name", product.ProductName);
            cmd.Parameters.AddWithValue("@price", product.ProductPrice);

            // Open connection if closed?
            if (dbConn.State == System.Data.ConnectionState.Closed) dbConn.Open();

            // Send query to DB and get last inserted Id
            product.ProductId = (int)cmd.ExecuteScalar();

            // Close connection if open?
            if (dbConn.State == System.Data.ConnectionState.Open) dbConn.Close();

            return product;
        }
    }
}
