using Microsoft.Data.SqlClient;
using SimpleORMDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleORMDemo.DataAccess
{
    public class ManufacturerORM : IManufacturerORM
    {
        private readonly SqlConnection _connection;

        public ManufacturerORM()
        {
            _connection = MSSQLConnection.GetConnection();
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            string query = "SELECT Id, Name FROM dbo.Manufacturer";
            SqlCommand cmd = new SqlCommand(query, _connection);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);


            while (reader.Read())
            {
                manufacturers.Add(new Manufacturer(reader.GetInt32(0), reader.GetString(1)));
            }

            //_connection.Close();
            reader.Close();

            return manufacturers;
        }

        public Manufacturer GetManufacturerById(int id)
        {
            Manufacturer manu = null;
            string query = "SELECT Id, Name FROM dbo.Manufacturer WHERE id = @value";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@value", id);

            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                manu = new Manufacturer(reader.GetInt32(0), reader.GetString(1));
            }

            if (manu == null)
            {
                Console.WriteLine("NO manufacturer with this ID exists");
            }

            //_connection.Close();
            reader.Close();

            return manu;
        }
    }
}
