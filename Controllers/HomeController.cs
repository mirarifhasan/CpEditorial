using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = new DBHelper().getConnectionString();

        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddContact(ContactModel contactModel)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Contact VALUES (@Name, @Email, @Message)";
                var sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Name", contactModel.name);
                sqlCmd.Parameters.AddWithValue("@Email", contactModel.email);
                sqlCmd.Parameters.AddWithValue("@Message", contactModel.message);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "Home");
        }
     
    }
}