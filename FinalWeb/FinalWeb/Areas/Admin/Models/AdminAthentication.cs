
using FinalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalWeb.Areas.Admin.Models
{
    public class AdminAthentication : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            BeverageRetailContext dbContext = new BeverageRetailContext();
            
            var employee = dbContext.Employees.SingleOrDefault(c => c.UserName == context.HttpContext.Session.GetString("UserName"));
            
            if (context.HttpContext.Session.GetString("UserName") == null || employee == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    
                    {"Controller", "Access"},
                    {"Action", "LoginAdmin"}
                });
            }

        }
    }
}
