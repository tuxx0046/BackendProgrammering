﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleORMDemo.DataAccess
{
    /// <summary>
    /// Class used to connect to MSSQL Server
    /// </summary>
    public static class MSSQLConnection
    {
        public static SqlConnection GetConnection(string database)
        {
            SqlConnection connection;
            string connectionString = $"Data Source=localhost;Initial Catalog={database};Integrated Security=True";
            connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
