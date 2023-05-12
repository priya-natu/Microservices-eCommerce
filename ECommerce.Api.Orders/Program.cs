using ECommerce.Api.Customers.DB;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Provider;
using ECommerce.Api.Orders.DB;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Provider;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseInMemoryDatabase("Orders");
});
builder.Services.AddScoped<IOrderProvider, OrderProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
