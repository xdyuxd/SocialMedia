using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        private string url = "https://localhost:44331/api";

        [HttpGet, OutputCache(Duration = 60 * 15, VaryByParam = "none")]
        public async Task<ActionResult> Index(string nickname)
        {
            ViewBag.Title = "Name - Profile";
            ViewBag.Name = "Name";
            ViewBag.Bio = "Escreva aqui sua biografia, um pouco de fatos particulares de sua vida.";
            if ((string)Session["Client"] == nickname)
            {
                Dictionary<string, object> client = (Dictionary<string, object>)Session["ClientCollection"];
                ViewBag.Title = $"{client["Name"]} - Profile";
                ViewBag.Name = client["Name"];
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync($"{url}/client/{nickname}");
                        response.EnsureSuccessStatusCode();
                        string r = await response.Content.ReadAsStringAsync();
                        if (r.Contains("Not Found."))
                        {
                            return RedirectToAction("Error");
                        }
                        else
                        {
                            var client_dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(r);
                            ViewBag.Title = $"{client_dict["Name"]} - Profile";
                            ViewBag.Name = client_dict["Name"];
                            ViewBag.Bio = client_dict["Bio"];
                        }
                    }
                }
                catch
                {

                    return RedirectToAction("Error");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult FriendList(string nickname)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["Client"] = null;
            return Json(new { url = Url.Action("Index", "Home") });
        }

        [HttpPost]
        public string GetCurrentClient()
        {
            if (Session["Client"] != null)
                return (string)Session["Client"];
            else
                return "Not Found.";
        }

        [HttpGet]
        public ActionResult Error()
        {
            ViewBag.Name = "Not found - SocialMedia";
            return View();
        }
    }
}