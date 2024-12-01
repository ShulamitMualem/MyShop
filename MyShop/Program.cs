using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMyRepository, MyRepository>();
builder.Services.AddScoped<IMyServices, MyServices>();
builder.Services.AddDbContext<MyShop328264650Context>(options => options.UseSqlServer("Server=SRV2\\PUPILS;Database=MyShop_328264650;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
