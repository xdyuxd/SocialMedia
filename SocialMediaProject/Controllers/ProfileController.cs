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
                Dictionary<string, object> client = (Dictionary<string, object>)Session["ClientCollection"];
                ViewBag.Title = $"{client["Name"]} - Profile";
                ViewBag.Name = client["Name"];
                ViewBag.Surname = client["Surname"];
                ViewBag.Bio = client["Bio"];
                ViewBag.Birthdate = client["Birthdate"];
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
        public string UpdateProfileCollection()
        {
            try
            {
                Dictionary<string, object> client = (Dictionary<string, object>)Session["ClientCollection"];
                Session["ClientCollection"] = new Dictionary<string, object>() {
                            { "Nickname", client["Nickname"] },
                            { "Name", Request.Headers.GetValues("Name")[0] ?? client["Client"] },
                            { "Surname", Request.Headers.GetValues("Surname")[0] },
                            { "Bio", Request.Headers.GetValues("Bio")[0]},
                            { "Profile_pic", Request.Headers.GetValues("Profile_pic") ?? client["Profile_pic"] },
                            { "Birthdate", Request.Headers.GetValues("Birthdate") ?? client["Birthdate"] }
                            };

                return "The Bio was updated!";
            }
            catch
            {
                return "Failed";
            }
        }

        [HttpPost]
        public ActionResult Search(string nickname)
        {
            return RedirectToAction("Index", nickname);
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
        public ActionResult Error()
        {
            ViewBag.Name = "Not found - SocialMedia";
            return View();
        }
    }
}