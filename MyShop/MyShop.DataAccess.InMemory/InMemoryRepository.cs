using MyShop.Core.Contract;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T:BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items = new List<T>();
        string className;
        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] =items;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tUpdate = items.Find(i => i.Id == t.Id);
            if (tUpdate == null)
            {
                throw new Exception(className + " not found");
            }
            else
            {
                tUpdate = t;
            }

        }
        public T Find(String id)
        {
            T tFind= items.Find(c => c.Id == id);
            if (tFind == null)
            {
                throw new Exception(className+" not found");
            }
            else
            {
                return tFind;
            }
        }
        public IQueryable<T> Collections()
        {
            return items.AsQueryable();
        }
        public void Delete(string id)
        {
            T tDelete = items.Find(c => c.Id == id);
            if (tDelete == null)
            {
                throw new Exception(className + " not found");
            }
            else
            {
                items.Remove(tDelete);
            }

        }
    }
}
