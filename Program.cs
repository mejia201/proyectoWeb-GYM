using Microsoft.EntityFrameworkCore;
using proyectoWeb_GYM.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

builder.Services.AddResponseCaching();


builder.Services.AddDbContext<gymDbContext>(opt =>
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("gymDbConnection"))
        );




var app = builder.Build();

app.UseSession();
app.UseResponseCaching();

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
