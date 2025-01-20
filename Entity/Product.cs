using System;
using System.Collections.Generic;


namespace Entity;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal Price { get; set; }

    public int CaregoryId { get; set; }

    public string? Description { get; set; }
    public string? ImgUrl { get; set; }

    public virtual Category Caregory { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
