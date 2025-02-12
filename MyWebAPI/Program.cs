using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;
using MyWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SomeService>();

//1.2.4 在Program.cs內以依賴注入的寫法撰寫讀取連線字串的物件
//      ※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後
builder.Services.AddDbContext<GoodStoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGoodStoreConnection")));

//4.7.7 在Program裡註冊GoodStoreContextCustom的Service(※注意※原本註冊的GoodStoreContext不可刪掉)
builder.Services.AddDbContext<GoodStoreContextCustom>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGoodStoreConnection")));

var app = builder.Build();

app.UseCors("MyCorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//2.1.3 在Program.cs中加入app.UseStaticFiles(); (因為我們開的是 純WebAPI專案)
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
