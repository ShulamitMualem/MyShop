using Entity;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using Repository.NewFolder;
using Repository.ProductsRepo;
using Repository.UserRepository;
using Services.OrderService;
using Services.ProductService;
namespace TestMyShop
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var user = new User() { UserName = "Shulamit", Password = "Shulamit!123" };

            var mockContext = new Mock<MyShop328264650Context>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userREpository = new MyRepository(mockContext.Object);

            var result = await userREpository.Login(user.UserName, user.Password);

            Assert.Equal(user, result);

        }
        [Fact]
        public async Task LogIn_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var mocContext = new Mock<MyShop328264650Context>();
            var users = new List<User>();
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            // Act
            var result = await userRepository.Login("nonExistentUser", "wrongPassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
            var mocContext = new Mock<MyShop328264650Context>();
            var users = new List<User> { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new MyRepository(mocContext.Object);

            // Act
            var result = await userRepository.Login(user.UserName, "wrongPassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateOrderSum_CHeckOrderSum_ReturnsOrder()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
            var order = new Order { OrderSum = 50, OrderItems = orderItems };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.CreateOrder(It.IsAny<Order>())).ReturnsAsync(order);

            List<Product> products = new List<Product> { new() { ProductId = 1, Price = 50 } };
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(products);
            var orderService = new OrderService(mockOrderRepository.Object,mockProductRepository.Object,null);

            // Act
            var result = await orderService.CreateOrder(order);

            // Assert
            Assert.Equal(order, result);
        }
        [Fact]
        public async Task CreateOrderSum_CHeckOrderSum_ReturnsNull()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
            var order = new Order { OrderSum = 45, OrderItems = orderItems };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.CreateOrder(It.IsAny<Order>())).ReturnsAsync(order);

            List<Product> products = new List<Product> { new() { ProductId = 1, Price = 50 } };
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(products);
            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object,null);

            // Act
            var result = await orderService.CreateOrder(order);

            // Assert
            Assert.Null(result);
        }
    }
    }