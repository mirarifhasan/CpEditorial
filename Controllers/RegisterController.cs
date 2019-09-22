using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (TempData["message"] != null) //It will true when user not login successfully
                ViewBag.Error = TempData["message"].ToString();
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("userID");
            Session.Remove("name");
            Session.Remove("point");
            Session.Remove("type");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VerifyUser(LoginModel loginModel)
        {
            // Check email registered or not
            var dtblUser = new DataTable();
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from [User] where Email = @email";
                var sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@email", loginModel.email);
                sqlDa.Fill(dtblUser);
            }
            //Ture - Found email in database
            if (dtblUser.Rows.Count == 1)
            {
                dtblUser.Clear();
                using (var sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "select * from [User] where Email = @email and Password = @password";
                    var sqlDa = new SqlDataAdapter(query, sqlCon);
                    sqlDa.SelectCommand.Parameters.AddWithValue("@email", loginModel.email);
                    sqlDa.SelectCommand.Parameters.AddWithValue("@password", loginModel.password);
                    sqlDa.Fill(dtblUser);
                }
                // If email and password both match
                if (dtblUser.Rows.Count == 1)
                {
                    Session["userID"] = dtblUser.Rows[0][0].ToString();
                    Session["name"] = dtblUser.Rows[0][1].ToString();
                    Session["point"] = Convert.ToInt32(dtblUser.Rows[0][4].ToString());
                    Session["type"] = dtblUser.Rows[0][5].ToString();

                    return RedirectToAction("Index", "Home");
                }
                // Email match but Password not match
                else
                {
                    TempData["message"] = "Password not match";
                    return RedirectToAction("LogIn", "Register");
                }
            }
            // Email not found in DB
            else
            {
                TempData["message"] = "Email not registered";
                return RedirectToAction("LogIn", "Register");
            }
        }

        public ActionResult SignUp()
        {
            if (TempData["message"] != null) //It will true when Password and ConfirmPassword not match
                ViewBag.Error = TempData["message"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(SignupModel signupModel)
        {
            // Check Password and ConfirmPassword are match or not
            if (signupModel.password != signupModel.confirmPassword)
            {
                TempData["message"] = "Password not match";
                return RedirectToAction("SignUp", "Register");
            }

            // Check the email already exist or not
            var dtblUser = new DataTable();
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from [User] where Email = @email";
                var sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@email", signupModel.email);
                sqlDa.Fill(dtblUser);
            }
            if(dtblUser.Rows.Count == 1)
            {
                TempData["message"] = "Email already registered";
                return RedirectToAction("SignUp", "Register");
            }

            // Insert the User
            using (var sqlCon = new SqlConnection(connectionString))
            {
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