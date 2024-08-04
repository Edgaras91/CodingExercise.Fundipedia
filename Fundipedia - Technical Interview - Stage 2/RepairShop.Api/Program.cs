using Microsoft.EntityFrameworkCore;
using RepairShop.Application.Repositories;
using RepairShop.Application.Services;
using RepairShop.Application.Services.Interfaces;
using RepairShop.Infrastructure.Persistence;
using RepairShop.Infrastructure.Persistence.Interfaces;
using RepairShop.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDbContext<IOrdersDbContext, OrdersDbContext>(options =>
{
    options.UseInMemoryDatabase("OrdersDB", x => x.EnableNullChecks(false));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var serviceScope = app.Services.CreateScope())
    {
        var ordersContext = serviceScope.ServiceProvider.GetRequiredService<OrdersDbContext>();
        TestDataSeeder.SeedData(ordersContext!);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();

public partial class Program
{
}