using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviationSystem.REST.Data;
using AviationSystem.REST.Models;
using Microsoft.EntityFrameworkCore;
namespace AviationSystem.REST.Services;


    /// <summary>
    /// Загальний інтерфейс для асинхронного CRUD-сервісу.
    /// </summary>
    public interface ICrudServiceAsync<T>
    {
        Task<IEnumerable<T>> ReadAllAsync();
        Task<T> ReadAsync(Guid id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task SaveAsync();
    }
public class EfCrudService<T> : ICrudServiceAsync<T>
        where T : AircraftModel
{
    private readonly ApplicationDbContext _ctx;
    private readonly DbSet<T> _db;

    public EfCrudService(ApplicationDbContext ctx)
    {
        _ctx = ctx;
        _db = ctx.Set<T>();
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
    {
        return await _db.ToListAsync();
    }

    public async Task<T> ReadAsync(Guid id)
    {
        return await _db.FindAsync(id);
    }

    public async Task<bool> CreateAsync(T entity)
    {
        if (entity.Id == Guid.Empty)
            entity.Id = Guid.NewGuid();
        await _db.AddAsync(entity);
        return true;
    }

    public Task<bool> UpdateAsync(T entity)
    {
        _db.Update(entity);
        return Task.FromResult(true);
    }

    public Task<bool> RemoveAsync(T entity)
    {
        _db.Remove(entity);
        return Task.FromResult(true);
    }

    public Task SaveAsync()
    {
        return _ctx.SaveChangesAsync();
    }
}