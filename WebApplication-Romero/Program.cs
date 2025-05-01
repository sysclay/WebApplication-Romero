using System;
using WebApplication_Romero.Services;
using Microsoft.EntityFrameworkCore;
using WebApplication_Romero.Context;

var builder = WebApplication.CreateBuilder(args);


//  ***************************************************
// Crear variable para la conexion
var connectionString = builder.Configuration.GetConnectionString("ConnectionDatabase");
// Registrar el servicio para la conexion
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
// ***********************************************  

builder.Services.AddHttpClient<UserService>();

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

app.Run();
