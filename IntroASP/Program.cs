using IntroASP.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// ==============================================================
// INYECCION DE DEPENDENCIA:
// inyectar DbContext, este incluye EntityFramework:
// La clase HLeonContext se creo automaticamte con el Scaffold.
// y esta en la clase Models.

builder.Services.AddDbContext<HLeonContext>(options => {

   options.UseSqlServer(
       builder.Configuration.GetConnectionString("conn_str")  
   );

});
// ==============================================================


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
