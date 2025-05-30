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
// 1) ��������� ���� � ����� ���� ������ ����������� (������� 2)
//    ���� ����� ���������� � �������� "Data" ����� � ����������� ������
var dbDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
if (!Directory.Exists(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
}
var dbFile = Path.Combine(dbDirectory, "aviation.db");
var connectionString = $"Data Source={dbFile}";
// -----------------------------------------------------------------------------

// 2) ������������ DbContext � �������������� SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
);

// 3) ������������ EF-������� CRUD
builder.Services.AddScoped<ICrudServiceAsync<MilitaryAircraftModel>, EfCrudService<MilitaryAircraftModel>>();
builder.Services.AddScoped<ICrudServiceAsync<CivilAircraftModel>, EfCrudService<CivilAircraftModel>>();
builder.Services.AddScoped<ICrudServiceAsync<CargoAircraftModel>, EfCrudService<CargoAircraftModel>>();

// 4) ��������� ����������� � Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5) ��������� �������� ��� ������ (�����������)
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