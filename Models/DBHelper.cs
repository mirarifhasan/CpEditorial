using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class DBHelper
    {
        public string connectionString = @"Data Source = DESKTOP-02U52QL\SQLEXPRESS; Initial Catalog = cpEditorial; Integrated Security=True";
        
        public string getConnectionString()
        {
            return connectionString;
        }
    }
}