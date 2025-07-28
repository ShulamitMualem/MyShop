using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public partial class User
{
    public int UserId { get; set; }
    [Required]
    [EmailAddress]
    public string UserName { get; set; } = null!;
    [StringLength(20, ErrorMessage = "Name can be till 20 letters")]
    public string? FirstName { get; set; }
    [StringLength(20, ErrorMessage = "Name can be till 20 letters")]
    public string? LastName { get; set; }
    [Required]
    public string Password { get; set; } = null!;
    public string? PasswordSalt { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

}
