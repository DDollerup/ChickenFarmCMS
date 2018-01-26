using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicNavbar.Factories;
using DynamicNavbar.Models;

namespace DynamicNavbar.Controllers
{
    public class HomeController : Controller
    {
        DBContext context = new DBContext();
        // GET: Home
        public ActionResult Index()
        {
            FrontPage frontPage = context.FrontpageFactory.Get(1);
            return View(frontPage);
        }

        [ChildActionOnly]
        public ActionResult DynamicNavbar()
        {
            List<Category> allCategories = context.CategoryFactory.GetAll();
            return PartialView(allCategories);
        }
    }
}