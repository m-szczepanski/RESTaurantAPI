using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RESTaurantAPI.Data;
using RESTaurantAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//app.UseCors(prodCorsPolicy);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//API services
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<DishService>();


//builder.Services.AddDbContext<APIDbContext>(options => options.UseInMemoryDatabase("localDb"));
builder.Services.AddDbContext<APIDbContext>(options => options.UseSqlServer("DatabaseConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/


app.UseSwagger();
app.UseSwaggerUI();

//app cors
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
