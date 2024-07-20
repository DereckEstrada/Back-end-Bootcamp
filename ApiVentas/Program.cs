using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Services;
using ApiVentas.Utilitarios;
using ejemploEntity.Services;
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
builder.Services.AddScoped<IBodegaServices, BodegaServices>();
builder.Services.AddScoped<IProductoServices, ProductoServices>();
builder.Services.AddScoped<IProveedorServices, ProveedorServices>();
builder.Services.AddScoped<IPuntoEmisionSriServices, PuntoEmisionSriServices>();
builder.Services.AddScoped<IPuntoVentaServices, PuntoVentaServices>();
builder.Services.AddScoped<IPais, PaisServices>();
builder.Services.AddScoped<IModulo, ModuloServices>();
builder.Services.AddScoped<IOpcion, OpcionServices>();
builder.Services.AddScoped<IMovimiento, MovimientoServices>();
builder.Services.AddScoped<IRolServices, RolServices>();
builder.Services.AddScoped<IStockServices, StockServices>();
builder.Services.AddScoped<IBodega, BodegaService>();
builder.Services.AddScoped<ICategoria, CategoriaService>();
builder.Services.AddScoped<ICiudad, CiudadService>();
builder.Services.AddScoped<ICliente, ClienteServices>();
builder.Services.AddScoped<IEmpresa, EmpresaService>();
builder.Services.AddScoped<IFormaPago, FormaPagoService>();
builder.Services.AddScoped<IIndustria, IndustriaService>();
builder.Services.AddDbContext<BaseErpContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
