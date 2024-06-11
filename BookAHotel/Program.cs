using BookAHotel.Data;
using BookAHotel.Logs;
using BookAHotel.Models;
using BookAHotel.Repository;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelBookingContext>(options =>
options.UseMySQL("Server=localhost;Database=hotelbooking;User=root;Password=Munmeo0503.;"));

builder.Services.AddScoped
    <IServiceRepository, ServiceRepository>();

builder.Services.AddLog4net();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) //add data into db
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
