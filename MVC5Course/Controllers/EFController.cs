﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable(); //AsQueryable 還不會取資料

            var data = all
                .Where(p => p.Active == true && p.Is刪除 == false && p.ProductName.Contains("Black"))
                .OrderByDescending(p => p.ProductId);

            //var data1 = all.Where(p => p.ProductId == 1); //是IQueryable list
            //var data2 = all.FirstOrDefault(p => p.ProductId == 1); //立刻取資料
            //var data3 = db.Product.Find(1); //立刻取資料

            return View(data);
        }


        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id); //立刻取資料
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product) //可使用網址 id 去取是那筆資料
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Stock = product.Stock;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id) //可以用簡單的方式，直接刪除，不用先導到刪除畫面
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Product.Find(id);

            //foreach (var item in product.OrderLine) //先刪除相關資料
            //{
            //    db.OrderLine.Remove(item);
            //}

            //db.OrderLine.RemoveRange(product.OrderLine);

            //db.Product.Remove(product);
            product.Is刪除 = true;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            //var data = db.Product.Find(id);
            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId=@p0", id).FirstOrDefault();
            return View(data);
        }

        public void RemoveAll()
        {
            //db.Product.RemoveRange(db.Product);
            //db.SaveChanges();

            db.Database.ExecuteSqlCommand("DELETE FROM dbo.Product");
        }
    }
}