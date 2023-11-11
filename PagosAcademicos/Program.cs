using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PagosAcademicos.Models.Entities;
using PagosAcademicos.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddDbContext<PagosacademicosContext>(
    optionBuilder => optionBuilder.UseMySql("server=localhost; user id=root; password=root; database=pagosAcademicos",
    ServerVersion.AutoDetect("server=localhost; user id=root; password=root; database=pagosAcademicos")));



builder.Services.AddTransient<Repository<Usuario>>();
builder.Services.AddTransient<Repository<Pago>>();


var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();
app.Run();
