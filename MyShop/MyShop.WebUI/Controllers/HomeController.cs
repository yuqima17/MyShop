using MyShop.Core.Contract;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public HomeController(IRepository<Product> context1, IRepository<ProductCategory> productCategories1)
        {

            context = context1;
            productCategories = productCategories1;

        }
        public ActionResult Index()
        {
            List<Product> products = context.Collections().ToList();
            return View(products);
        }
        public ActionResult Detail(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View();
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}