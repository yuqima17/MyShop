using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> categories = context.Collections().ToList();
            return View(categories);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                context.Insert(category);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit(string id)
        {
            ProductCategory categoryToEdit = context.Find(id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToEdit);
            }
            
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory category,string id)
        {
            ProductCategory categoryToEdit = context.Find(id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                categoryToEdit.Name = category.Name;
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string id)
        {
            ProductCategory categoryToDelete = context.Find(id);
            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
            
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory categoryToDelete = context.Find(id);
            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}