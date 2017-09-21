using System.Web.Mvc;

namespace Rent.Web.Areas.Rent
{
    public class RentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Rent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Rent_default",
                "Rent/{controller}/{action}/{id}",
                new { action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}