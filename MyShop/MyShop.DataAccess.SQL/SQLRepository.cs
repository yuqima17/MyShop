using MyShop.Core.Contract;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbset;
        public SQLRepository(DataContext context1)
        {
            this.context = context1;
            this.dbset = context.Set<T>();
        }
        public IQueryable<T> Collections()
        {
            return dbset;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var t = Find(id);
            if (context.Entry(t).State == EntityState.Detached)
            {
                dbset.Attach(t);
            }
            dbset.Remove(t);
        }

        public T Find(string id)
        {
            return dbset.Find(id);
        }

        public void Insert(T t)
        {
            dbset.Add(t);
        }

        public void Update(T t)
        {
            dbset.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
