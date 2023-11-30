using System;
using System.Collections.Generic;

namespace FinalWeb.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public string? Barcode { get; set; }

    public string? ProductImage { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
