using SocialMedia.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private string url = "https://localhost:44331/api"; //"https://socialmedia-api.azurewebsites.net/api";//"https://localhost:44331/api";
        private string url_create = "/client/create";

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            return View();

        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewForm model)
        {
            using (HttpClient client = new HttpClient())
            {
                TempData["Error"] = "An error occurred. Please try again later!";
                try
                {
                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url + url_create);
                    message.Headers.Add("name", model.Name);
                    message.Headers.Add("surname", model.Surname);
                    message.Headers.Add("email", model.Email);
                    message.Headers.Add("nickname", model.Nickname);
                    message.Headers.Add("password", model.Password);
                    message.Headers.Add("birthdate", (UnixTimestamp)model.Birthdate);
                    HttpResponseMessage response = client.SendAsync(message).GetAwaiter().GetResult();
                    if(response.IsSuccessStatusCode)
                        Session["Client"] = model.Nickname;
                    else
                        return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string uk,string password)
        {
            //uk = Email or Nickname
            try
            {
                uk = uk.ToLower();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{url}/client/{uk}");
                    response.EnsureSuccessStatusCode();
                    string r = await response.Content.ReadAsStringAsync();
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(r);
                    if (dict.ContainsValue(uk))
                    {
                        Session["Client"] = dict["Nickname"];
                        Session["ClientCollection"] = new Dictionary<string, object>() {
                            { "Nickname", dict["Nickname"] },
                            { "Name", dict["Name"] },
                            { "Surname", dict["Surname"] },
                            { "Bio", dict["Bio"] },
                            { "Profile_pic", dict["Profile_pic"] },
                            { "Birthdate", dict["Birthdate"] }
                            };
                    }
                    //TODO: Create POST to login and send encrypted key.
                }
            }
            catch
            {
                TempData["Error"] = "Credentials are incorrect! Check it again.";
                RedirectToAction("Index");
            }
            Debug.WriteLine(Session["Client"]);
            return RedirectToAction("Index", "Profile", new { nickname = Session["Client"] });
        }

    }
}