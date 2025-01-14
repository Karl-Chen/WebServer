using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//1.2.4 �bProgram.cs���H�̿�`�J���g�k���gŪ���s�u�r�ꪺ����
//      ���`�N�{������m�����n�bvar builder = WebApplication.CreateBuilder(args);�o�y����
builder.Services.AddDbContext<GuestBookContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGuestBookConnection")));

//5.3.3 �bProgram.cs�����U�αҥ�Session�A�s���������~�|�����A���]�i�H�]�wtimeout�Ҧp�n�J��5�����S�ʧ@�N�|���Ĥ������]�w�A�]�i�H��ʲM��
//���USession
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    //TimeOut�ɶ��A�M���N�O�Ҧ�Session�M��
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
///

//////-------------------------------------------------�o�橹�U�O�ҥΥ\��A���W�O���U-------------------------------------------
///
var app = builder.Build();

//1.3.4 �bProgram.cs���g�ҥ�Initializer���{��(�n�g�bvar app = builder.Build();����)
//      ���o��Initializer���@�άO�إߤ@�Ǫ�l��Ʀb��Ʈw���H�Q���աA�ҥH���@�w�n��Initializer��
//      ���`�N:��l��ƪ��Ӥ���bBookPhotos��Ƨ�����

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    SeedData.Initialize(service);
}

    // Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        //�ҥ~�o�ͮɪ��B�z����
        app.UseExceptionHandler("/Home/Error");
        //�U�����O�����䤣�줧�������~
        app.UseStatusCodePagesWithReExecute("/Home/Error");
    }
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//�ҥ�Session
app.UseSession();
//MapControllerRoute �w�]���|
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
