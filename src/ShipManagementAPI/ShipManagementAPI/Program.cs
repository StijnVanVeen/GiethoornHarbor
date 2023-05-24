using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShipManagementAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var sqlConnectionString = builder.Configuration.GetConnectionString("ShipManagementCN");
builder.Services.AddDbContext<ShipManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleManagement API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManagement API - v1");
});

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetService<ShipManagementDBContext>().MigrateDB();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();