using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioRol, UsuarioRolService>();
builder.Services.AddScoped<IUsuarioPermiso, UsuarioPermisoService>();
builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<ITipoMovimiento, TipoMovimientoService>();
builder.Services.AddScoped<ITarjetaCredito, TarjetaCreditoService>();
builder.Services.AddScoped<ISucursal, SucursalService>();

builder.Services.AddDbContext<BaseErpContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
