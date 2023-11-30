using System;
using System.Collections.Generic;

namespace FinalWeb.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string ContactName { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
