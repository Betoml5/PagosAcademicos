using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

string CadenaConexion = "server = sql.freedb.tech;user = freedb_FirtsUser; database = freedb_cliente; password = 7Z%@TYfXYnxjs4b";

builder.Services.AddDbContext<PagosacademicosContext>(
    optionBuilder => optionBuilder.UseMySql(CadenaConexion,
    ServerVersion.AutoDetect(CadenaConexion)));



builder.Services.AddTransient<Repository<Usuario>>();
builder.Services.AddTransient<Repository<Pago>>();
builder.Services.AddTransient<Repository<TipoPago>>();
builder.Services.AddTransient<TipoPagoRepository>();
builder.Services.AddTransient<PagoRepository>();
builder.Services.AddTransient<UsuarioRepository>();


var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();
app.Run();
