using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

public interface ICrudServiceAsync<T> : IEnumerable<T>
{
    Task<bool> CreateAsync(T element);
    Task<T> ReadAsync(Guid id);
    Task<IEnumerable<T>> ReadAllAsync();
    Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
    Task<bool> UpdateAsync(T element);
    Task<bool> RemoveAsync(T element);
    Task<bool> SaveAsync();
}

public class CrudServiceAsync<T> : ICrudServiceAsync<T>
{
    private List<T> _collection = new List<T>();
    private readonly string _filePath;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
    private readonly AutoResetEvent _saveEvent = new AutoResetEvent(true);

    public CrudServiceAsync(string filePath)
    {
        _filePath = filePath;
        LoadData().Wait();
    }

    private async Task LoadData()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonData;
                using (var streamReader = new StreamReader(_filePath)) // Класичний using для C# 7.3
                {
                    jsonData = await streamReader.ReadToEndAsync();
                }
                _collection = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            }
            catch
            {
                _collection = new List<T>();
            }
        }
    }

    public async Task<bool> CreateAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            _collection.Add(element);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<T> ReadAsync(Guid id)
    {
        _rwLock.EnterReadLock();
        try
        {
            return _collection.FirstOrDefault(e =>
                (Guid)e.GetType().GetProperty("Id").GetValue(e) == id);
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
    {
        _rwLock.EnterReadLock();
        try
        {
            return _collection.ToList();
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        _rwLock.EnterReadLock();
        try
        {
            return _collection
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToList();
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public async Task<bool> UpdateAsync(T element)
    {
        var id = (Guid)element.GetType().GetProperty("Id").GetValue(element);

        _rwLock.EnterWriteLock();
        try
        {
            var index = _collection.FindIndex(e =>
                (Guid)e.GetType().GetProperty("Id").GetValue(e) == id);

            if (index == -1) return false;

            _collection[index] = element;
            return true;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public async Task<bool> RemoveAsync(T element)
    {
        var id = (Guid)element.GetType().GetProperty("Id").GetValue(element);

        _rwLock.EnterWriteLock();
        try
        {
            var item = _collection.FirstOrDefault(e =>
                (Guid)e.GetType().GetProperty("Id").GetValue(e) == id);

            return item != null && _collection.Remove(item);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public async Task<bool> SaveAsync()
    {
        _saveEvent.WaitOne();
        try
        {
            using (var streamWriter = new StreamWriter(_filePath)) // Класичний using для C# 7.3
            {
                var jsonData = JsonConvert.SerializeObject(_collection);
                await streamWriter.WriteAsync(jsonData);
            }
            return true;
        }
        finally
        {
            _saveEvent.Set();
        }
    }

    public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Bus
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public int Capacity { get; set; }
    public double FuelConsumption { get; set; }

    public static Bus CreateNew()
    {
        var random = new Random();
        return new Bus
        {
            Id = Guid.NewGuid(),
            Model = "Bus-" + random.Next(1000, 9999),
            Capacity = random.Next(20, 100),
            FuelConsumption = Math.Round(random.NextDouble() * 10 + 10, 2)
        };
    }
}

class Program
{
    static async Task Main()
    {
        const string filePath = @"D:\Kod C#\Program2\buses.json";
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        var service = new CrudServiceAsync<Bus>(filePath);
        var stopwatch = Stopwatch.StartNew();

        Console.WriteLine("Початок генерації даних...");

        // Створення об'єктів паралельно (C# 7.3 сумісний спосіб)
        var tasks = new List<Task>();
        for (int i = 0; i < 1500; i++)
        {
            tasks.Add(Task.Run(async () => {
                var bus = Bus.CreateNew();
                await service.CreateAsync(bus);
            }));
        }
        await Task.WhenAll(tasks);

        Console.WriteLine($"Створено 1500 об'єктів за {stopwatch.ElapsedMilliseconds} мс");
        Console.WriteLine("Збереження даних у файл...");

        await service.SaveAsync();

        var allBuses = (await service.ReadAllAsync()).ToList();

        Console.WriteLine("\nРезультати аналізу:");
        Console.WriteLine($"Усього автобусів: {allBuses.Count}");
        Console.WriteLine($"Місткість:");
        Console.WriteLine($"  Мін: {allBuses.Min(b => b.Capacity)}");
        Console.WriteLine($"  Макс: {allBuses.Max(b => b.Capacity)}");
        Console.WriteLine($"  Середня: {allBuses.Average(b => b.Capacity):F2}");
        Console.WriteLine($"Споживання пального:");
        Console.WriteLine($"  Мін: {allBuses.Min(b => b.FuelConsumption):F2} л/100км");
        Console.WriteLine($"  Макс: {allBuses.Max(b => b.FuelConsumption):F2} л/100км");
        Console.WriteLine($"  Середнє: {allBuses.Average(b => b.FuelConsumption):F2} л/100км");

        Console.WriteLine($"\nФайл збережено: {filePath}");
        Console.WriteLine($"Загальний час виконання: {stopwatch.Elapsed.TotalSeconds:F2} сек");
    }
}