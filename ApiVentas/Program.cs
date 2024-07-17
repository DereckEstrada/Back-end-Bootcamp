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

builder.Services.AddDbContext<BaseErpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

var app = builder.Build();

// Verificar la conexión a la base de datos al inicio
try
{
    using (var db = new BaseErpContext())
    {
        db.Database.EnsureCreated();
        Console.WriteLine("Connection to database successful!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to database: {ex.Message}");
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
