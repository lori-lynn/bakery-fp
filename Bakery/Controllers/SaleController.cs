using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class SaleController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Sale
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> cusLiItem = from s in db.People
                                                 select new SelectListItem
                                                 {
                                                     Text = s.PersonFirstName + " " + s.PersonLastName,
                                                     Value = s.PersonKey.ToString()
                                                 };
            ViewBag.Customers = cusLiItem;

            IEnumerable<SelectListItem> empLiItem = from s in db.Employees
                                                    select new SelectListItem
                                                    {
                                                        Text = s.Person.PersonFirstName + " " + s.Person.PersonLastName,
                                                        Value = s.EmployeeKey.ToString()
                                                    };
            ViewBag.Employees = empLiItem;

            IEnumerable<SelectListItem> prodLiItem = from s in db.Products
                                                    select new SelectListItem
                                                    {
                                                        Text = s.ProductName,
                                                        Value = s.ProductKey.ToString()
                                                    };
            ViewBag.Products = prodLiItem;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CustomerKey, EmployeeKey")]Sale sl)

        {
            var resultMessage = new Message();
            if (ModelState.IsValid)
            {
                sl.SaleDate = DateTime.Now;
                var slDet = new SaleDetail();
                slDet.ProductKey = Int32.Parse(Request["Product"]);
                slDet.SaleDetailPriceCharged = db.Products.Find(Int32.Parse(Request["Product"])).ProductPrice;
                slDet.SaleDetailQuantity = Int32.Parse(Request["Quantity"]);
                slDet.SaleDetailDiscount = Int32.Parse(Request["Discount"]);
                slDet.SaleDetailSaleTaxPercent = 10;
                slDet.SaleDetailEatInTax = 20;
                sl.SaleDetails.Add(slDet);
                db.SaveChanges();
            }

            return View("Result", resultMessage);
        }
    }
}