using FinalWeb.Areas.Admin.Models;
using FinalWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/AdminLogin")]
     
    public class AdminLoginController : Controller
    {
        BeverageRetailContext db = new BeverageRetailContext();
        [Route("Login")]
        [HttpGet]

        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("productCategory", "HomeAdmin");
            }


        }
        [Route("Login")]
        [HttpPost]

        public IActionResult Login(Employee user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Employees.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    HttpContext.Session.SetString("FirstName", u.FirstName.ToString());
                    HttpContext.Session.SetString("EmployeeID", u.EmployeeId.ToString());
                    return RedirectToAction("productCategory", "HomeAdmin");
                }
                else
                {
                    ViewBag.LoginFail = "Login fail! Please try again!";
                    return View();
                }
            }
            return View();
        }
    }
}
