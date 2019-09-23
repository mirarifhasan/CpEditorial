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
            string sql = "INSERT INTO Contact VALUES ('"+ contactModel.name + "', '"+ contactModel.email + "', '"+ contactModel.message + "')";
            new DBHelper().setTable(sql);

            return RedirectToAction("Index", "Home");
        }
     
    }
}