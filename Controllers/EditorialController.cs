using CpEditorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CpEditorial.Controllers
{
    public class EditorialController : Controller
    {
        // GET: Post
        public ActionResult PostForm()
        {
            PostFormModel postEditorialModel = new PostFormModel();
            return View(postEditorialModel); // Return all tag and OJ list in a big outer list
        }

        [HttpPost]
        public ActionResult AddEditorial(PostFormModel postFormModel)
        {
            postFormModel.DateTime = Convert.ToString(DateTime.UtcNow);
            postFormModel.UserID = Convert.ToInt32(Session["userID"]);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ViewEditorial()
        {
            int editorialId = Convert.ToInt32(Request.QueryString["id"]);
            ViewEditorial viewEditorial = new ViewEditorial(editorialId);
            
            return View(viewEditorial);
        }
    }
}