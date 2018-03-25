using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class RegistrationController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Registration
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonLastName, PersonFirstName, PersonEmail, PersonPrimaryPhone, PersonPlainPassword")]NewPerson np)
        {
            int result = db.usp_Register(np.PersonLastName, np.PersonFirstName, np.PersonEmail, np.PersonPhone, np.PersonPlainPassword);

            var resultMessage = new Message();
            if (result != -1)
            {
                resultMessage.TitleText = "Success!";
                resultMessage.MessageText = "Thank you for registering! Please enjoy a tasty donut.";
            } else
            {
                resultMessage.TitleText = "Oh, snap!";
                resultMessage.MessageText = "There seems to have been an error. Please try again or give us a call!";
            }
            return View("Result", resultMessage);
        }
    }
}