using AutoMapper;
using DTO;
using Entity;
namespace MyShop
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<User, UserGetById>();
            CreateMap<CreateUser, User>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<OrderItemDTO, OrderItem>();
        }
    }
}
