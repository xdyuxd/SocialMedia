using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Profile(string nickname)
        {
            return View();
        }

        [HttpGet]
        public ActionResult FriendList(string nickname)
        {
            return View();
        }
    }
}