using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserGetById(string UserName, string FirstName, string LastName);
    public record CreateUser(
        [Required][EmailAddress] string UserName,
        [StringLength(100, ErrorMessage = "First name can be till 100 letters")] string FirstName,
        [StringLength(100, ErrorMessage = "Last name can be till 100 letters")] string LastName,
        [Required] string Password);

}
