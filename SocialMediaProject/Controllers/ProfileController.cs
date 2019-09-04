using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet, OutputCache(Duration = 60 * 15, VaryByParam = "none")]
        public ActionResult Index(string nickname)
        {
            ViewBag.Name = "Name";
            if ((string)Session["Client"] == nickname)
            {
                Dictionary<string, string> client = (Dictionary<string, string>)Session["ClientCollection"];
                ViewBag.Name = client["Name"];
            }
            else
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult FriendList(string nickname)
        {
            return View();
        }
    }
}