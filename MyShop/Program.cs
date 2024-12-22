using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.CategoryRepo;
using Repository.NewFolder;
using Repository.ProductsRepo;
using Repository.UserRepository;
using Services.CategoryService;
using Services.OrderService;
using Services.ProductService;
using Services.UserService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMyRepository, MyRepository>();
builder.Services.AddScoped<IMyServices, MyServices>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository >();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<MyShop328264650Context>(options => options.UseSqlServer("Server=SRV2\\PUPILS;Database=MyShop_328264650;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
