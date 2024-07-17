using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IBodega, BodegaService>();
builder.Services.AddScoped<ICategoria, CategoriaService>();
builder.Services.AddScoped<ICiudad, CiudadService>();
builder.Services.AddScoped<ICliente, ClienteServices>();
builder.Services.AddScoped<IEmpresa, EmpresaService>();
builder.Services.AddScoped<IFormaPago, FormaPagoService>();
builder.Services.AddScoped<IIndustria, IndustriaService>();

builder.Services.AddDbContext<BaseErpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

var app = builder.Build();

// Verificar la conexi�n a la base de datos al inicio
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
