using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalWeb.Models.Authentication
{
    public class Authentication :ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            BeverageRetailContext dbContext = new BeverageRetailContext();
            var customer = dbContext.Customers.SingleOrDefault(c => c.UserName == context.HttpContext.Session.GetString("UserName"));

            if (context.HttpContext.Session.GetString("UserName")== null || customer == null)
            {
               
               
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller","Access" },
                    {"Action","Login" }
                });
            }
         
        }
    }
}
