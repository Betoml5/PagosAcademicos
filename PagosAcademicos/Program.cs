using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddDbContext<PagosAcademicosContext>(
    optionBuilder => optionBuilder.UseMySql("server=localhost; user id=root; password=root; database=pagosAcademicos",
    ServerVersion.AutoDetect("server=localhost; user id=root; password=root; database=pagosAcademicos")));

builder.Services.AddTransient<UsuarioRepository>();
builder.Services.AddTransient<PagoRepository>();
builder.Services.AddSingleton<CarreraRepository>();
builder.Services.AddSingleton<SemestreRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();
app.Run();
