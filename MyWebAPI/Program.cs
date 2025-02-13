using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;
using MyWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//���s���F��
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
//8.2.3 �bProgram.cs�̵��UCategoryService
builder.Services.AddScoped<CategoryService>();

//9.2.3 �bProgram.cs�����UHttpClient����
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<PetAdoptionService>();

//1.2.4 �bProgram.cs���H�̿�`�J���g�k���gŪ���s�u�r�ꪺ����
//      ���`�N�{������m�����n�bvar builder = WebApplication.CreateBuilder(args);�o�y����
builder.Services.AddDbContext<GoodStoreContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGoodStoreConnection")));

//4.7.7 �bProgram�̵��UGoodStoreContextCustom��Service(���`�N���쥻���U��GoodStoreContext���i�R��)
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

//2.1.3 �bProgram.cs���[�Japp.UseStaticFiles(); (�]���ڭ̶}���O ��WebAPI�M��)
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
