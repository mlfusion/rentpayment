using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Rent.Web.App_Start;

namespace Rent.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Mapping Initialize
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //AreaRegistration.RegisterAllAreas("Rent");
            //ControllerBuilder.Current.DefaultNamespaces.Add("Rent.Web.Areas.Rent.RentAreaRegistration");

            //AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        protected void Application_Stop()
        {
            //Session["UID"] = null;
            //System.Web.Security.FormsAuthentication.SignOut();
        }


        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            //if (Session["UID"] == null)
            //{
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    Response.Redirect("~/Rent/Rent/Login");
            //}
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  
            Session["UID"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Session["UID"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            // Code that runs when an unhandled error occurs
            System.Exception exception = Server.GetLastError().GetBaseException();
            // Send Distribution Lists Email
            Business.Services.LogError.Insert(exception, Convert.ToInt32(Session["UID"]));
            // You've handled the error, so clear it. Leaving the server in an error state can cause unintended side effects as the server continues its attempts to handle the error.
            Server.ClearError();
            // Possible that a partially rendered page has already been written to response buffer before encountering error, so clear it.
            Response.Clear();
            // Code that runs when an unhandled error occurs
            Response.Redirect("~/Rent/Rent/Login", false);
        }

    }
}
