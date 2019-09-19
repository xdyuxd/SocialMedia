using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialMediaProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Create",
                url: "create",
                defaults: new { controller = "Home", action = "Create" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login" }
            );

            /* PROFILE START */

            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new { controller = "Profile", action = "Logout" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "{nickname}",
                defaults: new { controller = "Profile", action = "Index", nickname = "" }
            );

            routes.MapRoute(
                name: "Get Current Client",
                url: "session/getcurrentclient",
                defaults: new { controller = "Profile", action = "GetCurrentClient" }
           );

            routes.MapRoute(
                name: "Profile Bio Update",
                url: "{nickname}/update/bio",
                defaults: new { controller = "Profile", action = "UpdateProfileCollection" }
            );

            routes.MapRoute(
                name: "Profile Error",
                url: "{nickname}/error",
                defaults: new { controller = "Profile", action = "Error" }
            );

            routes.MapRoute(
                name: "Profile List Friend",
                url: "{nickname}/friend",
                defaults: new { controller = "Profile", action = "FriendList", nickname = UrlParameter.Optional }
            );

            /* PROFILE END */

        }
    }
}
