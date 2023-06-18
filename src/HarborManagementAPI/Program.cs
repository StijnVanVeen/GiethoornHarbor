using HarborManagementAPI.DataAccess;
using HarborManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add DBContext
var sqlConnectionString = builder.Configuration.GetConnectionString("HarborManagementCN");
builder.Services.AddDbContext<HarborManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

// Add services to the container.
builder.Services.AddScoped<IHarborManagementService, HarborManagementService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "HarborManagement API", Version = "v1" });
}) ; 

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