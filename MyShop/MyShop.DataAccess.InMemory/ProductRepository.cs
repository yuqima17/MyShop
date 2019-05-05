﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if(productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }

        }
        public Product Find(string Id1)
        {
            Product productFound = products.Find(p => p.Id == Id1);
            if (productFound != null)
            {
                return productFound;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public IQueryable<Product> Collections()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id1)
        {
            Product productToDelete = products.Find(p => p.Id == Id1);
            if(productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
