using FinalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;



namespace FinalWeb.Controllers
{
    public class AccessController : Controller
    {
        BeverageRetailContext db = new BeverageRetailContext();
        
        [HttpGet]
       
        public IActionResult Login()
        {
            
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
        [HttpPost]

        public IActionResult Login(Customer user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Customers.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    HttpContext.Session.SetString("ContactName", u.ContactName.ToString());
                    HttpContext.Session.SetString("CustomerID", u.CustomerId.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginFail = "Login fail! Please try again!";
                    return View("Login");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        //HTTP get/Home/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // Http post/Home/Register
        public IActionResult Register(Customer cus)
        {
            try
            {
                db.Customers.Add(cus);
                db.SaveChanges();

                // Set a success message in TempData
                TempData["SuccessMessage"] = "Registration successful! Please log in with your new account.";

                
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                TempData["ErrorMessage"] = "An error occurred during registration: " + ex.Message;
                
            }
            return RedirectToAction("Register");
        }
        [HttpGet]
        public IActionResult LoginAdmin()
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
       
        [HttpPost]

        public IActionResult LoginAdmin(Employee user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Employees.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    HttpContext.Session.SetString("FirstName", u.FirstName.ToString());
                    HttpContext.Session.SetString("LastName", u.LastName.ToString());
                    return RedirectToAction("productCategory", "Admin");
                }
                else
                {
                    ViewBag.LoginFail = "Login fail! Please try again!";
                    return View();
                }
            }
            return View();
        }
        public IActionResult LogoutAdmin()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("LoginAdmin", "Access");
        }

    }
}
