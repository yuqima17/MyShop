﻿using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contract
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collections();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T t);
        void Update(T t);
    }
}