using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AviationSystem.REST.Models;
using AviationSystem.REST.Services;
using AviationSystem.REST.Data;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------------------------
// 1) Формируем путь к файлу базы данных динамически (вариант 2)
//    Файл будет расположен в подпапке "Data" рядом с исполняемым файлом
var dbDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
if (!Directory.Exists(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
}
var dbFile = Path.Combine(dbDirectory, "aviation.db");
var connectionString = $"Data Source={dbFile}";
// -----------------------------------------------------------------------------

// 2) Регистрируем DbContext с использованием SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
);

// 3) Регистрируем EF-сервисы CRUD
builder.Services.AddScoped<ICrudServiceAsync<MilitaryAircraftModel>, EfCrudService<MilitaryAircraftModel>>();
builder.Services.AddScoped<ICrudServiceAsync<CivilAircraftModel>, EfCrudService<CivilAircraftModel>>();
builder.Services.AddScoped<ICrudServiceAsync<CargoAircraftModel>, EfCrudService<CargoAircraftModel>>();

// 4) Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5) Применяем миграции при старте (опционально)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// 6) Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();