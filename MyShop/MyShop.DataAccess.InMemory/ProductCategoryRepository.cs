using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> categories = new List<ProductCategory>();
        public ProductCategoryRepository()
        {
            categories=cache["categories"] as List<ProductCategory>;
            if (categories == null)
            {
                categories= new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["categories"] = categories;
        }
        public void Insert(ProductCategory category)
        {
            categories.Add(category);
        }
        public void Update(ProductCategory c)
        {
            ProductCategory categoryToUpdate = categories.Find(category => category.Id == c.Id);
            if (categoryToUpdate == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                categoryToUpdate = c;
            }

        }
        public ProductCategory Find(String id)
        {
            ProductCategory categoryToFind = categories.Find(c => c.Id == id);
            if (categoryToFind == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                return categoryToFind;
            }
        }
        public IQueryable<ProductCategory> Collections()
        {
            return categories.AsQueryable();
        }
        public void Delete(string id)
        {
            ProductCategory categoryToDelete = categories.Find(c => c.Id == id);
            if (categoryToDelete == null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                categories.Remove(categoryToDelete);
            }

        }
    }
    
    
}
