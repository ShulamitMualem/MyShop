using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using Repository.NewFolder;
using Repository.ProductsRepo;
using Repository.UserRepository;
using Services.OrderService;
namespace TestMyShop
{
    public class IntegrationTest:IClassFixture<DatabaseFixture>
    {
       private readonly MyShop328264650Context _context;
        public IntegrationTest(DatabaseFixture fixture)
        {
            _context =fixture.Context;
        }

        [Fact]
        public async Task Get_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { UserName = "test@example.com", Password = "password123" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var userREpository = new MyRepository(_context);


            // Act
            var retrievedUser = await userREpository.Login(user.UserName,user.Password);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserName, retrievedUser.UserName);
        }
        [Fact]
        public async Task LogIn_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var userRepository = new MyRepository(_context);

            // Act
            var result = await userRepository.Login("wrongPassword", "nonExistentUser");

            // Assert
            Assert.Null(result);  
        }

        [Fact]
        public async Task LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userRepository = new MyRepository(_context);

            // Act
            var result = await userRepository.Login("wrongPassword", user.UserName);

            // Assert
            Assert.Null(result); 
        }

        [Fact]
        public async Task CreateOrderSum_CHeckOrderSum_ReturnsOrder()
        {
            // Arrange
            _context.Categories.Add(new Category { CategoryName = "basic" });
            await _context.SaveChangesAsync();

            List<Product> products = new List<Product> { new() { Price = 6, ProductName = "Milk", CaregoryId=1 ,Description="tasty",ImgUrl="1.jpg"}, new() { Price = 20, ProductName = "eggs", CaregoryId=1, Description = "tasty", ImgUrl = "1.jpg" } };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            var orderItems = new List<OrderItem>() { new() { ProductId = 1 }, new() { ProductId = 2 } };
            var order = new Order { OrderSum = 26, OrderItems = orderItems };

            var orderService = new OrderService(new OrderRepository(_context) , new ProductsRepository(_context),null);

            // Act
            var result = await orderService.CreateOrder(order);

            // Assert
            Assert.Equal(order, result);
        }
        [Fact]
        public async Task CreateOrderSum_CHeckOrderSum_ReturnsNull()
        {
            // Arrange
            _context.Categories.Add(new Category { CategoryName = "basic" });
            await _context.SaveChangesAsync();

            List<Product> products = new(){ new() { Price = 6, ProductName = "Milk", CaregoryId = 1, Description = "tasty", ImgUrl = "1.jpg" }, new() { Price = 20, ProductName = "eggs", CaregoryId = 1, Description = "tasty", ImgUrl = "1.jpg" } };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            var orderItems = new List<OrderItem>() { new() { ProductId = 1 }, new() { ProductId = 2 } };
            var order = new Order { OrderSum = 50, OrderItems = orderItems };

            var mockIlloger = new Mock<ILogger<OrderService>>();

            var orderService = new OrderService(new OrderRepository(_context), new ProductsRepository(_context),mockIlloger.Object);

            // Act
            var result = await orderService.CreateOrder(order);

            // Assert
            Assert.Null(result);
        }
    }
}