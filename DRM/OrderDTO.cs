using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(int OrderId, DateOnly? OrserDate, decimal? OrderSum, string? UserUserName, List<OrderItemDTO> OrderItems);
    public record CreateOrderDTO(decimal? OrderSum, int UserId, List<OrderItemDTO> OrderItems);
    public record OrderItemDTO(int? ProductId, int? Quantity);
}
