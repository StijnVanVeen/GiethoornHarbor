using Microsoft.EntityFrameworkCore;
using SecurityManagementAPI.DataAccess;
using SecurityManagementAPI.IServices;
using SecurityManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DBContext
var sqlConnectionString = builder.Configuration.GetConnectionString("HarborSecurity");
builder.Services.AddDbContext<HarborSecurityDbContext>(options => options.UseSqlServer(sqlConnectionString));

// Add services to the container.
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<ITruckService, TruckService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();