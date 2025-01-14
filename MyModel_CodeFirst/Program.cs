using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//1.2.4 在Program.cs內以依賴注入的寫法撰寫讀取連線字串的物件
//      ※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後
builder.Services.AddDbContext<GuestBookContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGuestBookConnection")));

//5.3.3 在Program.cs中註冊及啟用Session，瀏覽器關掉才會消失，但也可以設定timeout例如登入後5分鐘沒動作就會失效之類的設定，也可以手動清掉
//註冊Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    //TimeOut時間，清掉就是所有Session清掉
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
///

//////-------------------------------------------------這行往下是啟用功能，往上是註冊-------------------------------------------
///
var app = builder.Build();

//1.3.4 在Program.cs撰寫啟用Initializer的程式(要寫在var app = builder.Build();之後)
//      ※這個Initializer的作用是建立一些初始資料在資料庫中以利測試，所以不一定要有Initializer※
//      ※注意:初始資料的照片放在BookPhotos資料夾中※

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    SeedData.Initialize(service);
}

    // Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        //例外發生時的處理頁面
        app.UseExceptionHandler("/Home/Error");
        //下面的是頁面找不到之類的錯誤
        app.UseStatusCodePagesWithReExecute("/Home/Error");
    }
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//啟用Session
app.UseSession();
//MapControllerRoute 預設路徑
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
