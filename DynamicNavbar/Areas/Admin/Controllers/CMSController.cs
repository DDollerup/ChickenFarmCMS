using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DynamicNavbar.Models;
using DynamicNavbar.Factories;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace DynamicNavbar.Areas.Admin.Controllers
{
    [Authorize]
    public class CMSController : Controller
    {
        DBContext context = new DBContext();

        [AllowAnonymous]
        public ActionResult Login(string returnurl)
        {
            TempData["ReturnURL"] = returnurl;
            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult LoginSubmit(string email, string password)
        {
            SHA512 pw = new SHA512Managed();
            pw.ComputeHash(Encoding.ASCII.GetBytes(password));
            string hashedPassword = BitConverter.ToString(pw.Hash).Replace("-", "").ToLower();

            Member member = context.MemberFactory.SqlQuery("SELECT * FROM Member WHERE Email='" + email + "' AND Password ='" + hashedPassword + "'");
            if (member.ID > 0)
            {
                FormsAuthentication.SetAuthCookie(member.Email, false);
                TempData["SYS_MSG"] = "Welcome to the CMS: " + member.Firstname + " " + member.Lastname;
                return Redirect(TempData["ReturnURL"].ToString());
            }
            TempData["SYS_MSG"] = "Wrong password or username. Try Again.";
            return RedirectToAction("Login");
        }

        public ActionResult LogoutSubmit()
        {
            FormsAuthentication.SignOut();
            TempData["SYS_MSG"] = "You have been logged out.";
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditFrontPage()
        {
            FrontPage frontPage = context.FrontpageFactory.Get(1);
            return View(frontPage);
        }

        [HttpPost]
        public ActionResult EditFrontPageSubmit(FrontPage frontPage, HttpPostedFileBase imageFile)
        {
            // ContentLength = filens størrelse i bytes.
            if (imageFile?.ContentLength > 0)
            {
                string serverPath = Request.PhysicalApplicationPath;
                string savePath = serverPath + @"/Content/Images/" + imageFile.FileName;
                imageFile.SaveAs(savePath);
                frontPage.Image = imageFile.FileName;
            }

            context.FrontpageFactory.Update(frontPage);
            TempData["SYS_MSG"] = "Frontpage has been updated.";

            return RedirectToAction("EditFrontPage");
        }

        #region Categories
        public ActionResult Categories()
        {
            List<Category> allCategories = context.CategoryFactory.GetAll();
            return View(allCategories);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategorySubmit(Category category)
        {
            if (context.CategoryFactory.ExistsBy("Name", category.Name) == false)
            {
                context.CategoryFactory.Insert(category);
                TempData["SYS_MSG"] = "A category has been created.";
                return RedirectToAction("Categories");
            }
            else
            {
                TempData["SYS_MSG"] = "A category with that name, already exists.";
                return RedirectToAction("AddCategory");
            }
        }

        // id = category id
        public ActionResult EditCategory(int id = 0)
        {
            Category category = context.CategoryFactory.Get(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategorySubmit(Category category)
        {
            context.CategoryFactory.Update(category);

            TempData["SYS_MSG"] = "A category has been updated.";

            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategorySubmit(int id = 0)
        {
            if (id < 1)
            {
                TempData["SYS_MSG"] = "There was an error with the function called.";
            }
            else
            {
                if (context.CategoryFactory.ExistsBy("ID", id) == true)
                {
                    context.CategoryFactory.Delete(id);
                    TempData["SYS_MSG"] = "A category has been deleted.";
                }
                else
                {
                    TempData["SYS_MSG"] = "No category was found with that ID";
                }
            }

            return RedirectToAction("Categories");
        }
        #endregion

        #region Products
        public ActionResult Products()
        {
            List<Product> allProducts = context.ProductFactory.GetAll();
            return View(allProducts);
        }

        public ActionResult AddProduct()
        {
            ViewBag.Categories = context.CategoryFactory.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddProductSubmit(Product product, HttpPostedFileBase imageFile)
        {
            // ContentLength = filens størrelse i bytes.
            if (imageFile?.ContentLength > 0)
            {
                string serverPath = Request.PhysicalApplicationPath;
                string savePath = serverPath + @"/Content/Images/" + imageFile.FileName;
                imageFile.SaveAs(savePath);
                product.Image = imageFile.FileName;
            }

            context.ProductFactory.Insert(product);
            TempData["SYS_MSG"] = "A product has been added.";

            return RedirectToAction("Products");
        }

        // id er Product ID
        public ActionResult EditProduct(int id = 0)
        {
            Product product = context.ProductFactory.Get(id);
            ViewBag.Categories = context.CategoryFactory.GetAll();
            return View(product);
        }


        [HttpPost]
        public ActionResult EditProductSubmit(Product product, HttpPostedFileBase imageFile)
        {
            // ContentLength = filens størrelse i bytes.
            if (imageFile?.ContentLength > 0)
            {
                string serverPath = Request.PhysicalApplicationPath;
                string savePath = serverPath + @"/Content/Images/" + imageFile.FileName;
                imageFile.SaveAs(savePath);
                product.Image = imageFile.FileName;
            }

            context.ProductFactory.Update(product);
            TempData["SYS_MSG"] = "A product has been updated.";

            return RedirectToAction("Products");
        }

        public ActionResult DeleteProductSubmit(int id = 0)
        {
            if (id < 1)
            {
                TempData["SYS_MSG"] = "There was an error with the function called.";
            }
            else
            {
                if (context.ProductFactory.ExistsBy("ID", id) == true)
                {
                    context.ProductFactory.Delete(id);
                    TempData["SYS_MSG"] = "A product has been deleted.";
                }
                else
                {
                    TempData["SYS_MSG"] = "No product was found with that ID";
                }
            }

            return RedirectToAction("Products");
        }
        #endregion

        #region Activities
        public ActionResult Activities()
        {
            List<Activity> allActivities = context.ActivityFactory.GetAll();
            return View(allActivities);
        }

        public ActionResult AddActivity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddActivitySubmit(Activity activity)
        {
            context.ActivityFactory.Insert(activity);
            TempData["SYS_MSG"] = "An activity has been created.";
            return RedirectToAction("Activities");
        }

        // id = activity id
        public ActionResult EditActivity(int id = 0)
        {
            Activity activity = context.ActivityFactory.Get(id);
            return View(activity);
        }

        [HttpPost]
        public ActionResult EditActivitySubmit(Activity activity)
        {
            context.ActivityFactory.Update(activity);
            TempData["SYS_MSG"] = "An activity has been updated.";
            return RedirectToAction("Activities");
        }

        public ActionResult DeleteActivitySubmit(int id = 0)
        {
            if (id < 1)
            {
                TempData["SYS_MSG"] = "There was an error with the function called.";
            }
            else
            {
                if (context.ActivityFactory.ExistsBy("ID", id) == true)
                {
                    context.ActivityFactory.Delete(id);
                    TempData["SYS_MSG"] = "An activity has been deleted.";
                }
                else
                {
                    TempData["SYS_MSG"] = "No activity was found with that ID";
                }
            }
            return RedirectToAction("Activities");
        }
        #endregion

        #region Articles
        public ActionResult Articles()
        {
            List<Article> allArticles = context.ArticleFactory.GetAll();
            return View(allArticles);
        }

        public ActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddArticleSubmit(Article article, HttpPostedFileBase imageFile)
        {
            // ContentLength = filens størrelse i bytes.
            if (imageFile?.ContentLength > 0)
            {
                string serverPath = Request.PhysicalApplicationPath;
                string savePath = serverPath + @"/Content/Images/" + imageFile.FileName;
                imageFile.SaveAs(savePath);
                article.Image = imageFile.FileName;
            }

            article.Date = DateTime.Now;

            context.ArticleFactory.Insert(article);
            TempData["SYS_MSG"] = "An article has been created.";

            return RedirectToAction("Articles");
        }

        // id = article id
        public ActionResult EditArticle(int id = 0)
        {
            Article article = context.ArticleFactory.Get(id);
            return View(article);
        }

        [HttpPost]
        public ActionResult EditArticleSubmit(Article article, HttpPostedFileBase imageFile)
        {
            // ContentLength = filens størrelse i bytes.
            if (imageFile?.ContentLength > 0)
            {
                string serverPath = Request.PhysicalApplicationPath;
                string savePath = serverPath + @"/Content/Images/" + imageFile.FileName;
                imageFile.SaveAs(savePath);
                article.Image = imageFile.FileName;
            }

            context.ArticleFactory.Update(article);
            TempData["SYS_MSG"] = "An article has been updated.";

            return RedirectToAction("Articles");
        }

        public ActionResult DeleteArticleSubmit(int id = 0)
        {
            if (id < 1)
            {
                TempData["SYS_MSG"] = "There was an error with the function called.";
            }
            else
            {
                if (context.ArticleFactory.ExistsBy("ID", id) == true)
                {
                    context.ArticleFactory.Delete(id);
                    TempData["SYS_MSG"] = "An article has been deleted.";
                }
                else
                {
                    TempData["SYS_MSG"] = "No article was found with that ID";
                }
            }
            return RedirectToAction("Articles");
        }
        #endregion
    }
}