using Microsoft.EntityFrameworkCore;
using MyShop.Middleware;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repository;
using Repository.CategoryRepo;
using Repository.NewFolder;
using Repository.ProductsRepo;
using Repository.RatingRepository;
using Repository.UserRepository;
using Services.CategoryService;
using Services.OrderService;
using Services.ProductService;
using Services.RatingService;
using Services.UserService;


var builder = WebApplication.CreateBuilder(args);

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

string connectionString;

if (environment == "Home")
{
    connectionString = builder.Configuration.GetConnectionString("Home");
}
else if (environment == "Development")
{
    connectionString = builder.Configuration.GetConnectionString("School");
}
else
{
    throw new Exception("Unknown environment");
}

// Add services to the container.
builder.Services.AddScoped<IMyRepository, MyRepository>();
builder.Services.AddScoped<IMyServices, MyServices>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository >();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddDbContext<MyShop328264650Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();
app.UseRatingMiddleware();
app.MapControllers();

app.Run();
