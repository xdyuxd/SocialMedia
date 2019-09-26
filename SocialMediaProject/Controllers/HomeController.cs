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
        public object Create(ClientViewForm model)
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
                    if (response.IsSuccessStatusCode) { 
                        Session["Client"] = model.Nickname;
                        Session["ClientCollection"] = new Dictionary<string, object>() {
                                { "Nickname", model.Nickname },
                                { "Name",  model.Name },
                                { "Surname", model.Surname },
                                { "Bio", null },
                                { "Profile_pic", null },
                                { "Cover_pic", null },
                                { "Birthdate", (UnixTimestamp)model.Birthdate }
                        };
                    }
                    else
                        return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Profile", new { nickname = Session["Client"] });
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
                    if (dict.ContainsValue(uk) && dict.ContainsValue(password))
                    {
                        Session["Client"] = dict["Nickname"];
                        Session["ClientCollection"] = new Dictionary<string, object>() {
                            { "Nickname", dict["Nickname"] },
                            { "Name", dict["Name"] },
                            { "Surname", dict["Surname"] },
                            { "Bio", dict["Bio"] },
                            { "Profile_pic", dict["Profile_pic"] },
                            { "Cover_pic", dict["Cover_pic"] },
                            { "Birthdate", dict["Birthdate"] }
                            };
                    }
                    else
                    {
                        TempData["Error"] = "Credentials are incorrect! Check it again.";
                        RedirectToAction("Index");
                    }
                    //TODO: Create POST to login and send encrypted key.
                }
            }
            catch
            {
                TempData["Error"] = "Credentials are incorrect! Check it again.";
                RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Profile", new { nickname = Session["Client"] });
        }

    }
}