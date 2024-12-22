using System;
using System.Collections.Generic;


namespace Entity;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? OrserDate { get; set; }

    public decimal? OrderSum { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }
}
