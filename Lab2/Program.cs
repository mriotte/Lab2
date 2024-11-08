using Lab2.DAL;
using Lab2.DAL.Settings;
using Lab2.Services; // Додаємо простір імен для вашого DatabaseInitializer
using LabSolution.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Додайте DbContext iз налаштуваннями рядка пiдключення
builder.Services.AddDbContext<LabDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Конфiгурацiя MongoDB Settings
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDBSettings"));
// Реєстрацiя MongoDB-клiєнта

builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Додайте сервіс для ініціалізації бази даних
builder.Services.AddScoped<DatabaseInitializer>();

builder.Services.AddSingleton<StudentService>();

builder.Services.AddSingleton<IMongoService, MongoService>();
builder.Services.AddHostedService<UpdateService>();
// Додайте сервіси для контролерів та OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Запуск ініціалізації бази даних під час першого запуску
using (var scope = app.Services.CreateScope())
{
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await databaseInitializer.Initialize(); // Викликаємо ініціалізацію бази даних
}

// Якщо середовище - розробка, додаємо OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
