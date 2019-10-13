using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            int userID = Convert.ToInt32(Request.QueryString["uid"]);

            if (userID != Convert.ToInt32(Session["userID"]))
                return Redirect("/Warning/Index");

            ProfileModel profileModel = new ProfileModel(userID);
            return View(profileModel);
        }
    }
}