﻿using MyShop.Core.Contract;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService:IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;
        public const string BasketSessionName = "eCommerceBasket";
        BasketService(IRepository<Product> productContext1, IRepository<Basket> basketContext1)
        {
            this.productContext = productContext1;
            this.basketContext = basketContext1;
        }
        private Basket getBasket(HttpContextBase httpContext,bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket = new Basket();
            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }

            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }

            }
            return basket;
        }
        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);
            return basket;

        }
        public void AddToBasket(HttpContextBase httpContext,string productId)
        {
            Basket basket = getBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                item = new BasketItem();
                item.BasketId = basket.Id;
                item.ProductId = productId;
                item.Quantity = 1;
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity+1;
            }
            basketContext.Commit();
        }
        public void RemoveFromBasket(HttpContextBase httpContext,string itemId)
        {
            Basket basket = getBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }

            

        }
        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = getBasket(httpContext, false);
            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                              join p in productContext.Collections() on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ProductName = p.Name,
                                  Price = p.price,
                                  Image = p.Image
                              }).ToList();
                return results;

            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }
        public BasketSummaryViewModel GetBacketSummary(HttpContextBase httpContext)
        {
            Basket basket = getBasket(httpContext, false);
            BasketSummaryViewModel viewModel = new BasketSummaryViewModel(0,0);
            if (basket != null)
            {
                int basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();
                decimal basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collections()
                                        on item.ProductId equals p.Id
                                        select item.Quantity * p.price).Sum();
                viewModel.BasketCount = basketCount;
                viewModel.BasketTotal = basketTotal;
                return viewModel;
            }
            else
            {
                return viewModel;
            }

            

        }

    }
}
