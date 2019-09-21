using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Controllers
{
    public class RegisterController : Controller
    {
        string connectionString = new DBHelper().getConnectionString();

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            if (TempData["message"] != null)
                ViewBag.Error = TempData["message"].ToString();
            return View();
        }

        // It execute/call from AddUser method, when Password and ConfirmPassword will not match
        public ActionResult Verify()
        {
            TempData["message"] = "Password not match";
            return RedirectToAction("SignUp", "Register");
        }

        [HttpPost]
        public ActionResult AddUser(SignupModel signupModel)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                if (signupModel.password != signupModel.confirmPassword)
                    return RedirectToAction("Verify", "Register");

                sqlCon.Open();
                string query = "INSERT INTO [User] VALUES (@Name, @Email, @Password, '0', 'User')";
                var sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Name", signupModel.name);
                sqlCmd.Parameters.AddWithValue("@Email", signupModel.email);
                sqlCmd.Parameters.AddWithValue("@Password", signupModel.password);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Home");            
        }
        
    }
}