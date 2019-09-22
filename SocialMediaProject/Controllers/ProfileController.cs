using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        private string url = "https://localhost:44331/api";

        [HttpGet]
        public async Task<ActionResult> Index(string nickname)
        {
            ViewBag.Title = "Name - Profile";
            ViewBag.Name = "Name";
            ViewBag.Bio = "Escreva aqui sua biografia, um pouco de fatos particulares de sua vida.";

            if ((string)Session["Client"] == nickname)
            {
                using (HttpClient c = new HttpClient())
                {
                    HttpResponseMessage response = await c.GetAsync($"{url}/client/{Session["Client"]}");
                    response.EnsureSuccessStatusCode();
                    string r = await response.Content.ReadAsStringAsync();
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(r);

                    ViewBag.Title = $"{dict["Name"]} - Profile";
                    ViewBag.Name = dict["Name"];
                    ViewBag.Surname = dict["Surname"];
                    ViewBag.Bio = dict["Bio"];
                    ViewBag.Birthdate = dict["Birthdate"];
                    ViewBag.Cover_pic = dict["Cover_pic"];
                    ViewBag.Profile_pic = dict["Profile_pic"];
                }


                using (HttpClient c = new HttpClient())
                {
                    HttpResponseMessage response = await c.GetAsync($"{url}/client/{(string)Session["Client"]}/gallery");
                    response.EnsureSuccessStatusCode();
                    string r = await response.Content.ReadAsStringAsync();
                    var dict = JsonConvert.DeserializeObject<List<object>>(r);
                    ViewBag.Gallery = dict;
                }
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

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{url}/client/{(string)Session["Client"]}/notification");
                response.EnsureSuccessStatusCode();
                string r = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<List<object>>(r);
                ViewBag.Notification = dict;
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{url}/client/{(string)Session["Client"]}/friendship");
                response.EnsureSuccessStatusCode();
                string r = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<List<object>>(r);
                ViewBag.Friendship = dict;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {
            return RedirectToAction("Index", new { nickname = Request.Form["nickname"] });
        }

        [HttpPost]
        public string GetCurrentClient()
        {
            if (Session["Client"] != null)
                return (string)Session["Client"];
            else
                return "Not Found.";
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["Client"] = null;
            return Json(new { url = Url.Action("Index", "Home") });
        }

        [HttpGet]
        public async Task<ActionResult> Error()
        {
            ViewBag.Name = "Not found - SocialMedia";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{url}/client/{(string)Session["Client"]}/notification");
                response.EnsureSuccessStatusCode();
                string r = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<List<object>>(r);
                ViewBag.Notification = dict;
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{url}/client/{(string)Session["Client"]}/friendship");
                response.EnsureSuccessStatusCode();
                string r = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<List<object>>(r);
                ViewBag.Friendship = dict;
            }

            return View();
        }
    }
}