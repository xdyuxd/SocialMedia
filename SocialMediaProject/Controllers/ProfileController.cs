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
            ViewBag.Name = "Name - Profile";
            ViewBag.Bio = "Escreva aqui sua biografia, um pouco de fatos particulares de sua vida.";
            if ((string)Session["Client"] == nickname)
            {
                Dictionary<string, string> client = (Dictionary<string, string>)Session["ClientCollection"];
                ViewBag.Name = $"client['Name'] - Profile";
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
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
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

        [HttpGet]
        public ActionResult Error()
        {
            ViewBag.Name = "Not found - SocialMedia";
            return View();
        }
    }
}