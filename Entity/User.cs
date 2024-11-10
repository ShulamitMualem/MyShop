using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [StringLength(20, ErrorMessage = "Name can be till 20 letters")]
        public string firstName { get; set; }
        [StringLength(20, ErrorMessage = "Name can be till 20 letters")]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
