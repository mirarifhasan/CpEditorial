﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class DBHelper
    {
        public string connectionString = @"Data Source = DESKTOP-1TLCT0C\SQLEXPRESS1; Initial Catalog = CpEditorial; Integrated Security=True";
        
        public DataTable getTable(string sql)
        {
            var dtbl = new DataTable();
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                var sqlDa = new SqlDataAdapter(sql, sqlCon);
                sqlDa.Fill(dtbl);
                return dtbl;
            }
        }

        public void setTable(string sql)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                var sqlCmd = new SqlCommand(sql, sqlCon);
                sqlCmd.ExecuteNonQuery();
            }
        }

    }
}