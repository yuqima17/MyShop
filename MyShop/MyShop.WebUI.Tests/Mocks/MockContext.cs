using MyShop.Core.Contract;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<T>:IRepository<T> where T:BaseEntity
    {
        List<T> items;
        string className;
        public MockContext()
        {
            this.items=new List<T>();
        }
        public void Commit()
        {
            return;
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
            T tFind = items.Find(c => c.Id == id);
            if (tFind == null)
            {
                throw new Exception(className + " not found");
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
