using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CpEditorial.Models;

namespace CpEditorial.Controllers
{
    public class AccountController : Controller
    {
        // GET: Dashboard
        public ActionResult Account( Account ac)
        {
            string connectionString = @"Data Source = DESKTOP-1TLCT0C\SQLEXPRESS1; Initial Catalog = CpEditorial; Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(connectionString);

            string query = "select UserName, Email, Point , UserType from [User]";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Connection = sqlCon;

            sqlCon.Open();
            SqlDataReader sdr = sqlCmd.ExecuteReader();

            List<Account> objModel = new List<Account>();

            if (sdr.Read())
            {
                var details = new Account();
                details.UserName = sdr["UserName"].ToString();
                details.Email = sdr["Email"].ToString();
                details.Point = Convert.ToInt32 (sdr["Point"].ToString());
                details.UserType = sdr["UserType"].ToString();
                objModel.Add(details);

                ac.UserInfo = objModel;
                sqlCon.Close();

            }



            return View("Account", ac);
        }
    }
}