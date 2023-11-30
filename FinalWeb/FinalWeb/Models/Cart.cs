using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FinalWeb.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double TotalAmount { get; set; }
    public Product Product { get; set; }

    public static void Add(int productID, int customerID, string productName, int quantity, double price)
    {
        using (var db = new BeverageRetailContext())
        {
            var existingCart = db.Carts
                .Where(c => c.CustomerId == customerID && c.ProductId == productID)
                .FirstOrDefault();

            if (existingCart != null)
            {
                // Call the function to update quantity
                int updatedQuantity = existingCart.Quantity + quantity;
                Update(productID, customerID, updatedQuantity, price, productName);
            }
            else
            {
                Cart cart = new Cart()
                {

                    CustomerId = customerID,
                    ProductId = productID,
                    Quantity = quantity,
                    Price = price,
                    ProductName = productName,
                    TotalAmount = price * quantity
                };

                // Assuming db.Carts is your DbSet<Cart> in the DbContext
                db.Carts.Add(cart);
                db.SaveChanges();
            }
        }
    }
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

    public static void Delete(int productId, int accountId)
    {
        using (var db = new BeverageRetailContext())
        {
            // Assuming there is a GioHang entity in your context
            var cartItemToDelete = db.Carts
               .FirstOrDefault(g => g.ProductId == productId && g.CustomerId == accountId);

            if (cartItemToDelete != null)
            {
                // Delete the cart item
                db.Carts.Remove(cartItemToDelete);

                // Save changes to the database
                db.SaveChanges();
            }

        }
    }
    public static double Total(int customerID)
    {
        using (var db = new BeverageRetailContext())
        {
            List<Cart> a = GetCartItems(customerID).ToList();
            if (a.Count() == 0)
            {
                return 0;
            }
            return db.Carts.Where(c => c.CustomerId == customerID).Sum(c => c.TotalAmount);
        }
    }
    public static IEnumerable<Cart> GetCartItems(int customerId)
    {
        using (var db = new BeverageRetailContext())
        {
            // Assuming there is a Cart entity in your context
            var cartItems = db.Carts
                .Where(c => c.CustomerId == customerId)
                .ToList();

            return cartItems;
        }
    }

}

