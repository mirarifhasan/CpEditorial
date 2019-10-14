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
    public class HomeController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            string query = "select top 10 Editorial.EditorialID, Editorial.UpVote, Editorial.DownVote, Editorial.Solution, Problem.Title from Editorial, Problem, [User] where Editorial.ProblemID=Problem.ProblemID AND Editorial.UserID=[User].UserID;";
            DataTable dtblUser = new DBHelper().getTable(query);
            return View(dtblUser);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["userName"] != null) {
                ContactModel contactModel = new ContactModel();
                return View(contactModel);
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult AddContact(ContactModel contactModel)
        {
            string sql = "INSERT INTO Feedback VALUES ('"+ contactModel.name + "', '"+ contactModel.email + "', '"+ contactModel.message + "')";
            new DBHelper().setTable(sql);

            return RedirectToAction("Index", "Home");
        }
     
    }
}