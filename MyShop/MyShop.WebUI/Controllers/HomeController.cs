using MyShop.Core.Contract;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
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
        public ActionResult Index(string Category=null)
        {
            List<Product> productsToShow;
            List<ProductCategory> categories = productCategories.Collections().ToList();
            if (Category == null)
            {
                productsToShow = context.Collections().ToList();
            }
            else
            {
                productsToShow = context.Collections().Where(p => p.category == Category).ToList();
            }
            ProductListViewModel model = new ProductListViewModel();
            model.products = productsToShow;
            model.productCategories = categories;
            return View(model);
        }
        public ActionResult Detail(string id1)
        {
            Product product = context.Find(id1);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
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