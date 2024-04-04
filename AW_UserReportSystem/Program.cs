using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AW_UserReportSystem.Data;
using AW_UserReportSystem.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AW_UserReportSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AW_UserReportSystemContext") ?? throw new InvalidOperationException("Connection string 'AW_UserReportSystemContext' not found.")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using(var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
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
