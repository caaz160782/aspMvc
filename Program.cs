using Microsoft.EntityFrameworkCore;
using AspMvcNet.Data;
var builder = WebApplication.CreateBuilder(args);

//configuramos la conexion a sql server}
var conexionString = builder.Configuration.GetConnectionString("DefaultConnection")??
throw new InvalidOperationException("error alc onectarse a la bd");

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(conexionString));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//error 404
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.Run();
