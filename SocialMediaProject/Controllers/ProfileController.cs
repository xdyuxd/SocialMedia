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
        private string url = "https://localhost:44331/api"; //"https://socialmedia-api.azurewebsites.net/api"; //"https://localhost:44331/api";

        [HttpGet]
        public async Task<ActionResult> Index(string nickname)
        {
            ViewBag.Title = "Name - Profile";
            ViewBag.Name = "Name";
            ViewBag.Bio = "Escreva aqui sua biografia, um pouco de fatos particulares de sua vida.";
            ViewBag.Cover_pic = "iVBORw0KGgoAAAANSUhEUgAABdwAAAFeCAAAAAC6aYJ4AAAACXBIWXMAAA7EAAAOxAGVKw4bAAAJyWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDUgNzkuMTYzNDk5LCAyMDE4LzA4LzEzLTE2OjQwOjIyICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtbG5zOnRpZmY9Imh0dHA6Ly9ucy5hZG9iZS5jb20vdGlmZi8xLjAvIiB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDE5LTA5LTIwVDIxOjAxOjQ1LTAzOjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAxOS0wOS0yMlQyMDo1NTo1My0wMzowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAxOS0wOS0yMlQyMDo1NTo1My0wMzowMCIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjEiIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJEb3QgR2FpbiAyMCUiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NThiZDgwZGEtOTM4Ny01MTRjLWJmOGEtYTYxMWM5YmFiNTBhIiB4bXBNTTpEb2N1bWVudElEPSJhZG9iZTpkb2NpZDpwaG90b3Nob3A6ZTFiYjc5ZDMtMmJlYS0yYzRjLWI0M2QtOWMxMzI0ODIxMWU2IiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6Y2UwOGFhMmYtNzI4MS1iYTQwLWEzOTMtM2RlNzk0OTJhYzhmIiB0aWZmOk9yaWVudGF0aW9uPSIxIiB0aWZmOlhSZXNvbHV0aW9uPSI5NjAwMDAvMTAwMDAiIHRpZmY6WVJlc29sdXRpb249Ijk2MDAwMC8xMDAwMCIgdGlmZjpSZXNvbHV0aW9uVW5pdD0iMiIgZXhpZjpDb2xvclNwYWNlPSI2NTUzNSIgZXhpZjpQaXhlbFhEaW1lbnNpb249IjE1MDAiIGV4aWY6UGl4ZWxZRGltZW5zaW9uPSIzNTAiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE + IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmNlMDhhYTJmLTcyODEtYmE0MC1hMzkzLTNkZTc5NDkyYWM4ZiIgc3RFdnQ6d2hlbj0iMjAxOS0wOS0yMFQyMTowMTo0NS0wMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTkgKFdpbmRvd3MpIi8 + IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDpjMTNjMzYzOS0yMjRlLTMyNGMtYmI2MC0yOWFjMDAwZWNmYTciIHN0RXZ0OndoZW49IjIwMTktMDktMjJUMjA6NTU6NTMtMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY29udmVydGVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJmcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nIi8 + IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJkZXJpdmVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJjb252ZXJ0ZWQgZnJvbSBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIHRvIGltYWdlL3BuZyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6NThiZDgwZGEtOTM4Ny01MTRjLWJmOGEtYTYxMWM5YmFiNTBhIiBzdEV2dDp3aGVuPSIyMDE5LTA5LTIyVDIwOjU1OjUzLTAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPC9yZGY6U2VxPiA8L3htcE1NOkhpc3Rvcnk + IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOmMxM2MzNjM5LTIyNGUtMzI0Yy1iYjYwLTI5YWMwMDBlY2ZhNyIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpjZTA4YWEyZi03MjgxLWJhNDAtYTM5My0zZGU3OTQ5MmFjOGYiIHN0UmVmOm9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDpjZTA4YWEyZi03MjgxLWJhNDAtYTM5My0zZGU3OTQ5MmFjOGYiLz4gPC9yZGY6RGVzY3JpcHRpb24 + IDwvcmRmOlJERj4gPC94OnhtcG1ldGE + IDw / eHBhY2tldCBlbmQ9InIiPz59AUrbAAAD0ElEQVR42u3UMQ0AAAzDsOItfwBlsWOyIeRICsA7kQDA3AEwdwDMHQBzB8DcAcwdAHMHwNwBMHcAzB3A3AEwdwDMHQBzB8DcATB3AHMHwNwBMHcAzB0AcwcwdwDMHQBzB8DcATB3AMwdwNwBMHcAzB0AcwfA3AHMHQBzB8DcATB3AMwdAHMHMHcAzB0AcwfA3AEwdwBzB8DcATB3AMwdAHMHwNwBzB0AcwfA3AEwdwDMHcDcATB3AMwdAHMHwNwBMHcAcwfA3AEwdwDMHQBzBzB3AMwdAHMHwNwBMHcAzB3A3AEwdwDMHQBzB8DcAcwdAHMHwNwBMHcAzB0AcwcwdwDMHQBzB8DcATB3AHMHwNwBMHcAzB0AcwfA3AHMHQBzB8DcATB3AMwdwNwBMHcAzB0AcwfA3AEwdwBzB8DcATB3AMwdAHMHMHcAzB0AcwfA3AEwdwDMHcDcATB3AMwdAHMHwNwBzB0AcwfA3AEwdwDMHQBzBzB3AMwdAHMHwNwBMHcAcwfA3AEwdwDMHQBzB8DcAcwdAHMHwNwBMHcAzB3A3AEwdwDMHQBzB8DcATB3AHMHwNwBMHcAzB0AcwcwdwDMHQBzB8DcATB3AMwdwNwBMHcAzB0AcwfA3AHMHQBzB8DcATB3AMwdAHMHMHcAzB0AcwfA3AEwdwBzB8DcATB3AMwdAHMHwNwBzB0AcwfA3AEwdwDMHcDcATB3AMwdAHMHwNwBMHcAcwfA3AEwdwDMHQBzBzB3AMwdAHMHwNwBMHcAzB3A3AEwdwDMHQBzB8DcAcwdAHMHwNwBMHcAzB0AcwcwdwDMHQBzB8DcATB3AHMHwNwBMHcAzB0AcwfA3AHMHQBzB8DcATB3AMwdwNwBMHcAzB0AcwfA3AEwdwBzB8DcATB3AMwdAHMHMHcAzB0AcwfA3AEwdwDMHcDcATB3AMwdAHMHwNwBzB0AcwfA3AEwdwDMHcDcJQAwdwDMHQBzB8DcATB3AHMHwNwBMHcAzB0AcwcwdwDMHQBzB8DcATB3AMwdwNwBMHcAzB0AcwfA3AHMHQBzB8DcATB3AMwdAHMHMHcAzB0AcwfA3AEwdwBzB8DcATB3AMwdAHMHwNwBzB0AcwfA3AEwdwDMHcDcATB3AMwdAHMHwNwBMHcAcwfA3AEwdwDMHQBzBzB3AMwdAHMHwNwBMHcAzB3A3AEwdwDMHQBzB8DcAcwdAHMHwNwBMHcAzB0AcwcwdwDMHQBzB8DcATB3AHMHwNwBMHcAzB0AcwfA3AHMHQBzB8DcATB3AMwdwNwBMHcAzB2ASwOCs1iQ+dDLiQAAAABJRU5ErkJggg==";
            ViewBag.Profile_pic = "";
            ViewBag.Featured_photos = null;


            if ((string)Session["Client"] == nickname)
            {
                using (HttpClient c = new HttpClient())
                {
                    HttpResponseMessage response = await c.GetAsync($"{url}/client/{Session["Client"]}");
                    response.EnsureSuccessStatusCode();
                    string r = await response.Content.ReadAsStringAsync();
                    var client_dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(r);

                    ViewBag.Title = $"{client_dict["Name"]} - Profile";
                    ViewBag.Name = client_dict["Name"];
                    ViewBag.Surname = client_dict["Surname"];
                    ViewBag.Bio = client_dict["Bio"];
                    ViewBag.Birthdate = client_dict["Birthdate"];
                    if (client_dict["Cover_pic"] != null) 
                        ViewBag.Cover_pic = client_dict["Cover_pic"];
                    if (client_dict["Profile_pic"] != null)
                        ViewBag.Profile_pic = client_dict["Profile_pic"];

                    response = await c.GetAsync($"{url}/client/{Session["Client"]}/featuredphotos");
                    response.EnsureSuccessStatusCode();
                    r = await response.Content.ReadAsStringAsync();
                    var dict = JsonConvert.DeserializeObject<object>(r);
                    ViewBag.Featured_photos = dict;



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
                            if (client_dict["Cover_pic"] != null)
                                ViewBag.Cover_pic = client_dict["Cover_pic"];
                            if (client_dict["Profile_pic"] != null)
                                ViewBag.Profile_pic = client_dict["Profile_pic"];

                            response = await client.GetAsync($"{url}/client/{nickname}/featuredphotos");
                            response.EnsureSuccessStatusCode();
                            r = await response.Content.ReadAsStringAsync();
                            var dict = JsonConvert.DeserializeObject<object>(r);
                            ViewBag.Featured_photos = dict;
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