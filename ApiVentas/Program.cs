using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IBodega, BodegaServices>();
builder.Services.AddScoped<ICategoria, CategoriaServices>();
builder.Services.AddScoped<ICiudad, CiudadServices>();
builder.Services.AddScoped<ICliente, ClienteServices>();
builder.Services.AddScoped<IEmpresa, EmpresaServices>();
builder.Services.AddScoped<IFormaPago, FormaPagoServices>();
builder.Services.AddScoped<IIndustria, IndustriaServices>();

builder.Services.AddDbContext<BaseErpContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();