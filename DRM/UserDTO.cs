using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserGetById(string UserName, string FirstName, string LastName, List<OrderDTO> Orders);
    public record CreateUser( string UserName, string FirstName, string LastName, string Password);
        
}
