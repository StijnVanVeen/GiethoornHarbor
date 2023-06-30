using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Repositories;
using HarborManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using HarborManagementAPI.Messaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logContext) => 
    logContext
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithMachineName()
);

// add DBContext
var sqlConnectionString = builder.Configuration.GetConnectionString("HarborManagementCN");
builder.Services.AddDbContext<HarborManagementSQLDBContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.UseRabbitMQMessagePublisher(builder.Configuration);

// Add services to the container.

builder.Services.AddScoped<IHarborCommandRepository, SqlServerHarborCommandRepository>();
builder.Services.AddScoped<IHarborQueryRepository, MongoHarborQueryRepository>();
builder.Services.AddScoped<IShipQueryRepository, MongoRefDataQueryRepository>();
builder.Services.AddScoped<IEventStoreRepository, MongoEventStoreRepository>();
builder.Services.AddScoped<IShipContext, ShipRefMongoContext>();
builder.Services.AddScoped<IHarborContext, HarborMongoContext>();
builder.Services.AddScoped<IEventStoreContext, EventStoreMongoContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HarborManagement API", Version = "v1" });
}) ; 

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HarborManagement API - v1");
});

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetService<HarborManagementSQLDBContext>().MigrateDB();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();