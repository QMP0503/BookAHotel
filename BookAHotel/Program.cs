using BookAHotel.Data;
using BookAHotel.Logs;
using BookAHotel.Models;
using BookAHotel.Repository;
using BookAHotel.Repository.IRepository;
using BookAHotel.Service;
using BookAHotel.Service.IService;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pkix;
using System.Text.Json;
using Dapper;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.WriteIndented = true; // Pretty-print JSON
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddDbContext<HotelBookingContext>(options =>
options.UseMySQL("Server=localhost;Database=hotelbooking;User=root;Password=Munmeo0503.;"));

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IFindRepository<Client>,ClientRepository>();
builder.Services.AddScoped<IFindRepository<Booking>,BookingRepository>();
builder.Services.AddScoped<IFindRepository<Room>, RoomRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IStoreProceduresService, StoreProceduresService>();

builder.Services.AddAutoMapper(typeof(Program));

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
