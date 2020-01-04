using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SocialMediaProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;

            if (context.Session["Client"] == null)
            {
                if (context.Request.Url.AbsolutePath != "/" &&
                    context.Request.Url.AbsolutePath != "/create" &&
                    context.Request.Url.AbsolutePath != "/login")
                {
                    context.Response.Redirect("/");
                }
            }
            else
            {
                if (context.Request.Url.AbsolutePath == "/" ||
                    context.Request.Url.AbsolutePath == "/create" ||
                    context.Request.Url.AbsolutePath == "/login")
                {
                    context.Response.Redirect($"/{context.Session["Client"]}");
                }

                //TODO: Something more?
            }

        }
    }
}
