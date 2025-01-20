using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(DateOnly? OrserDate, decimal? OrderSum, string? UserUserName);
    public record CreateOrderDTO(decimal? OrderSum, int UserId,List<OrderItemDTO> OrderItem);
    public record OrderItemDTO(int? ProductId, int? Quantity);
}
