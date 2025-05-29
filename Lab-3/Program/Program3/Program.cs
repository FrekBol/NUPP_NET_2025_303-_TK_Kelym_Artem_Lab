using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Program3.Models;
using Program3.Repositories;

namespace Program3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Запуск транспортної системи...");

            // Шлях до бази даних SQLite
            var dbPath = @"D:\Kod C#\Program\transport.db";

            // Налаштування DbContext
            var optionsBuilder = new DbContextOptionsBuilder<TransportSystemContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            using (var context = new TransportSystemContext(optionsBuilder.Options))
            {
                // Застосування міграцій (автоматичне створення БД)
                await context.Database.MigrateAsync();

                // Створення репозиторіїв
                var busRepository = new EfRepository<BusModel>(context);
                var driverRepository = new EfRepository<DriverModel>(context);
                var routeRepository = new EfRepository<RouteModel>(context);

                Console.WriteLine("Генерація тестових даних...");

                // Створення маршруту
                var route = new RouteModel { Name = "Маршрут #1" };
                await routeRepository.AddAsync(route);

                // Створення автобусів та водіїв
                var random = new Random();
                for (int i = 0; i < 5; i++)
                {
                    var bus = new BusModel
                    {
                        Model = $"Bus-{random.Next(1000, 9999)}",
                        Capacity = random.Next(20, 50),
                        RouteModelId = route.Id
                    };
                    await busRepository.AddAsync(bus);

                    var driver = new DriverModel
                    {
                        Name = $"Водій-{i + 1}",
                        BusModelId = bus.Id
                    };
                    await driverRepository.AddAsync(driver);
                }

                Console.WriteLine("\nОтримання даних з бази:");

                // Отримання всіх автобусів
                var buses = (await busRepository.GetAllAsync()).ToList();
                foreach (var bus in buses)
                {
                    Console.WriteLine($"Автобус {bus.Model}, місткість: {bus.Capacity}");

                    // Отримання водія для автобуса
                    var driver = (await driverRepository.GetAllAsync())
                        .FirstOrDefault(d => d.BusModelId == bus.Id);

                    if (driver != null)
                    {
                        Console.WriteLine($" - Водій: {driver.Name}");
                    }
                }

                // Статистика
                Console.WriteLine($"\nУсього автобусів: {buses.Count}");
                Console.WriteLine($"Середня місткість: {buses.Average(b => b.Capacity):F0}");

                // Збереження змін (якщо були)
                await context.SaveChangesAsync();
            }

            Console.WriteLine("\nПрограма завершена. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}