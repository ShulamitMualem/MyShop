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
        [StringLength(20, ErrorMessage = "First name can be till 20 letters")] string FirstName,
        [StringLength(20, ErrorMessage = "Last name can be till 20 letters")] string LastName,
        [Required] string Password);

}
