using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories;

public class CargosRepository:IRepository<Cargo>
{
    private readonly DatabaseContext _context;
    
    public CargosRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Cargo> GetAsync(Expression<Func<Cargo, bool>> condition)
    {
        return await _context.Cargos
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<Cargo>> GetAllAsync()
    {
        return await _context.Cargos
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }

    public async Task<List<Cargo>> GetAllAsync(Expression<Func<Cargo, bool>> condition)
    {
        return await _context.Cargos
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<Cargo>> GetLastAsync(Expression<Func<Cargo, bool>> condition, int limit)
    {
        return await _context.Cargos
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<Cargo>> GetFirstAsync(Expression<Func<Cargo, bool>> condition, int limit)
    {
        return await _context.Cargos
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<Cargo> items)
    {
        await _context.AddRangeAsync(items);
    }

    public Task UpdateAsync(Cargo item)
    {
        _context.Cargos.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<Cargo, bool>> condition)
    {
        IEnumerable<Cargo> removeItems = _context.Cargos.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}