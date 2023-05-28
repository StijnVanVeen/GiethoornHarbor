using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pitstop.Infrastructure.Messaging.Configuration;
using Serilog;
using ShipsManagementAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// setup logging
//builder.Host.UseSerilog((context, logContext) => 
 //   logContext
  //      .ReadFrom.Configuration(builder.Configuration)
 //       .Enrich.WithMachineName()
//);

var sqlConnectionString = builder.Configuration.GetConnectionString("ShipsManagementCN");
builder.Services.AddDbContext<ShipsManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

// add messagepublisher
builder.Services.UseRabbitMQMessagePublisher(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShipsManagement API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShipsManagement API - v1");
});

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetService<ShipsManagementDBContext>().MigrateDB();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();