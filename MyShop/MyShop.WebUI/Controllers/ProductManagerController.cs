using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contract;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController(IRepository<Product> context1, IRepository<ProductCategory> productCategories1)
        {
            
            context = context1;
            productCategories = productCategories1;

        }
        // GET: ProductManager
        public ActionResult Index()
        {
            
            List<Product> products = context.Collections().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel1 = new ProductManagerViewModel();
            viewModel1.product = new Product();
            viewModel1.productCategories = productCategories.Collections();
            return View(viewModel1);
        }
        [HttpPost]
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(string id1)
        {
            
            Product product= context.Find(id1);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel2 = new ProductManagerViewModel();
                viewModel2.product = product;
                viewModel2.productCategories = productCategories.Collections();
                return View(viewModel2);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Product product,string id1,HttpPostedFileBase file)
        {
            Product productFound = context.Find(id1);
            if (productFound == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(productFound);
                }
                else
                {
                    if (file != null)
                    {
                        productFound.Image = id1 + Path.GetExtension(file.FileName);

                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + productFound.Image);
                    }
                    
                    productFound.Name = product.Name;
                    productFound.Description = product.Description;
                    productFound.category = product.category;
                    productFound.price = product.price;
 
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(String id1)
        {
            Product productToDelete = context.Find(id1);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);

            }
            
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String id1)
        {
            Product productToDelete = context.Find(id1);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id1);
                context.Commit();
                return RedirectToAction("Index");

            }

        }
    }
}