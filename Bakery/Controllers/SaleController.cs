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
        public SaleController()
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
        }
        // GET: Sale
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SaleKey")]string sk)

        {
            var resultMessage = new Message();
            Sale sl;
            if (Request["SaleKey"] == "")
            {
                sl = new Sale();
                sl.SaleDate = DateTime.Now;
                db.Sales.Add(sl);
                
            }
            else
            {
                sl = db.Sales.Find(Int32.Parse(Request["SaleKey"]));
            }
            
            if (ModelState.IsValid)
            {
                
                var slDet = new SaleDetail();
                sl.CustomerKey = Int32.Parse(Request["CustomerKey"]);
                sl.EmployeeKey = Int32.Parse(Request["EmployeeKey"]);
                slDet.ProductKey = Int32.Parse(Request["Products"]);
                slDet.SaleDetailPriceCharged = db.Products.Find(Int32.Parse(Request["Products"])).ProductPrice;
                slDet.SaleDetailQuantity = Request["Quantity"] == "" ? 0 : Int32.Parse(Request["Quantity"]);
                slDet.SaleDetailDiscount = Request["Discount"] == "" ? 0 : Int32.Parse(Request["Discount"]);
                slDet.SaleDetailSaleTaxPercent = 10;
                slDet.SaleDetailEatInTax = 20;
                sl.SaleDetails.Add(slDet);
                db.SaveChanges();

                if (Request["addmore"] == "Confirm and Add Another Product")
                { 
                    return View(sl);
                }
                else
                {
                    db.Entry(sl).Reference(r => r.Person).Load();
                    db.Entry(sl).Reference(r => r.Employee).Load();
                    db.Entry(sl.Employee).Reference(r => r.Person).Load();

                    return View("Receipt", sl);
                }
            }

            else
            { 
                resultMessage.MessageText = "We're so sorry, something seems to have gone wrong. Please try again.";
                return View("Result", resultMessage);
            }
        }
    }    
}