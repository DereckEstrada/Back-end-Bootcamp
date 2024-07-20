using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddDbContext<BaseErpContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
