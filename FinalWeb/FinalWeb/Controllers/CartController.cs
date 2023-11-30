using FinalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using FinalWeb.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using System.IO.Pipelines;

namespace FinalWeb.Controllers
{
    [Authentication]
   
    public class CartController : Controller
    {
        BeverageRetailContext db = new BeverageRetailContext();

        public ActionResult Index()
        {
            ViewBag.ShowFullScreenButton = false;
            try
            {
                int customerId = int.Parse(HttpContext.Session.GetString("CustomerID"));
                List<Cart> customerCartItems = db.Carts
                        .Where(cart => cart.CustomerId == customerId)
                        .ToList();
                foreach (var cartItem in customerCartItems)
                {
                    cartItem.Product = db.Products
                        .FirstOrDefault(product => product.ProductId == cartItem.ProductId);
                  
                }
                // Calculate the total using the Total method from GetCart class
                double cartTotal = Cart.Total(customerId);

                // Pass the total to the view
                ViewBag.CartTotal = cartTotal;

                return View(customerCartItems);
            }
            catch (Exception ex)
            {
                // Product was not successfully added to the cart
                TempData["CartAddErrorMessage"] = ex.Message;
                return RedirectToAction("Login", "Access");
            }
        }
        //ViewBag.Total = Cart.Total(int.Parse(User.Identity.GetUserId()));
        //return View(Cart.GetCartItems(int.Parse(User.Identity.GetUserId())));
        [HttpPost]
       // [ValidateAntiForgeryToken] // Add this attribute to help prevent cross-site request forgery attacks
        
        public ActionResult Add(int productID, string productName, int quantity, double price)
        {

            if (HttpContext.Session.GetString("UserName") == null)
            {
                // Customer is not logged in, redirect to login page
                return RedirectToAction("Details", "Home", new { ProductId = productID });
            }

            try
            {
                int customerId = int.Parse(HttpContext.Session.GetString("CustomerID"));
                Cart.Add(productID, customerId, productName, quantity, price);

                // Product was successfully added to the cart
                TempData["CartAddMessage"] = "Product successfully added to cart.";
            }
            catch (Exception ex)
            {
                // Product was not successfully added to the cart
                TempData["CartAddErrorMessage"] = ex.Message;
            }

            // Redirect to cart page
            return RedirectToAction("Details", "Home", new { ProductId = productID });
        }
        [HttpPost]
        public static void Update(int ProductID, int CustomerID, int Quantity, double price, string ProductName)
        {
            using (var db = new BeverageRetailContext())
            {
                var existingCartItem = db.Carts
                    .FirstOrDefault(c => c.ProductId == ProductID && c.CustomerId == CustomerID);

                if (existingCartItem != null)
                {
                    // Update the existing entity
                    existingCartItem.Quantity = Quantity;
                    existingCartItem.Price = price;
                    existingCartItem.ProductName = ProductName;
                    existingCartItem.TotalAmount = Quantity * price;

                    // Mark the entity as modified
                    db.Entry(existingCartItem).State = EntityState.Modified;

                    // Save changes
                    db.SaveChanges();
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int masanpham)
        {
            try
            {
                int customerId = int.Parse(HttpContext.Session.GetString("CustomerID"));
                Cart.Delete(masanpham, customerId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or show an error message
                ViewBag.ErrorMessage = "An error occurred while processing your request."+ ex;
                return RedirectToAction("Index");
            }

        }
        public ActionResult PlaceOrder()
        {
            ViewBag.ShowFullScreenButton = false;
            try
            {
                // Get the customer ID from the session
                int customerId = int.Parse(HttpContext.Session.GetString("CustomerID"));

                // Retrieve cart items and total amount
                List<Cart> cartItems = Cart.GetCartItems(customerId).ToList();
                double cartTotal = Cart.Total(customerId);

                // Set customer name for the view
                ViewBag.cartTotal = cartTotal;

                // Place order logic goes here (store order information in the database, send confirmation email, etc.)

                // Clear the shopping cart after placing the order
                using (var db = new BeverageRetailContext())  // Replace YourDbContext with your actual DbContext class
                {
                    var customerCartItems = db.Carts.Where(c => c.CustomerId == customerId).ToList();
                    db.Carts.RemoveRange(customerCartItems);
                    db.SaveChanges();
                }

                // Display the order confirmation view
                return View(cartItems);
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or show an error message
                ViewBag.ErrorMessage = "An error occurred while processing your order.";
                return RedirectToAction("Index", "Cart");
            }
        }



    }
}
