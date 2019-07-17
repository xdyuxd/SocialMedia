using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMedia.DAL;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private SocialMediaContext db = new SocialMediaContext();

        [HttpGet]
        public ActionResult Home()
        {
            return View("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nickname,Name,Surname,Birthdate,Password,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                
                db.clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Home");
            }
            return View("Index");
        }


    }
}