using InventorySystem.DataAccess;
using InventorySystem.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
string sqlConnection = builder.Configuration.GetConnectionString("InventoryDbCon");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(sqlConnection));

builder.Services.AddScoped<ProductoServices>();
builder.Services.AddScoped<InventarioServices>();
builder.Services.AddScoped<SucursalService>();


var app = builder.Build();
using var scope = app.Services.CreateScope();
using (AppDbContext ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    await ctx.Database.EnsureCreatedAsync();
    await ctx.Database.MigrateAsync();
}
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
