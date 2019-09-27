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

        public ActionResult ViewEditorial()
        {
            return View();
        }
    }
}