using Lab2.DAL;
using Lab2.DAL.Settings;
using Lab2.Services; // ������ ������ ���� ��� ������ DatabaseInitializer
using LabSolution.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ������� DbContext i� �������������� ����� �i���������
builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ����i�����i� MongoDB Settings
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDBSettings"));
// �������i� MongoDB-��i����

builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// ������� ����� ��� ����������� ���� �����
builder.Services.AddScoped<DatabaseInitializer>();

builder.Services.AddSingleton<StudentService>();

builder.Services.AddSingleton<IMongoService, MongoService>();
builder.Services.AddHostedService<UpdateService>();
// ������� ������ ��� ���������� �� OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// ������ ����������� ���� ����� �� ��� ������� �������
using (var scope = app.Services.CreateScope())
{
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await databaseInitializer.Initialize(); // ��������� ����������� ���� �����
}

// ���� ���������� - ��������, ������ OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
