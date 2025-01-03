using Microsoft.EntityFrameworkCore;
using MyModel_DBFirst.Models;

var builder = WebApplication.CreateBuilder(args);
//6.1.4 在Program.cs加入使用appsettings.json中的連線字串程式碼(這段必須寫法var builder這行後面，var app的前面)
builder.Services.AddDbContext<dbStudentsContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbStudentsConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();

//把服務都建在builder上之後再由builder去建構APP


////////////////////////////////////////我是分隔線//////////////////////////////////////////////////////
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
