﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collections().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string id1)
        {
            
            Product product= context.Find(id1);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Product product,string id1)
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
                    productFound.Name = product.Name;
                    productFound.Description = product.Description;
                    productFound.category = product.category;
                    productFound.price = product.price;
                    productFound.Image = product.Image;
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