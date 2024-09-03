using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.BaseImplementation.Repositories;

public class TransportationRepository:IRepository<Transportation>
{
    private readonly DatabaseContext _context;
    
    public TransportationRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Transportation> GetAsync(Expression<Func<Transportation, bool>> condition)
    {
        return await _context.Transportations
            .Include(item => item.Driver)
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrderStatusRecords)
            .Include(item => item.RoutePoints)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<Transportation>> GetAllAsync()
    {
        return await _context.Transportations
            .Include(item => item.Driver)
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrderStatusRecords)
            .Include(item => item.RoutePoints)
            .ToListAsync();
    }

    public async Task<List<Transportation>> GetAllAsync(Expression<Func<Transportation, bool>> condition)
    {
        return await _context.Transportations
            .Include(item => item.Driver)
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrderStatusRecords)
            .Include(item => item.RoutePoints)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }
    
    public async Task<List<Transportation>> GetAllAsync(List<Expression<Func<Transportation, bool>>> conditions)
    {
        IQueryable<Transportation> trucks = _context.Transportations
            .Include(item => item.Driver)
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrderStatusRecords)
            .Include(item => item.RoutePoints);
        
        foreach (var condition in conditions)
        {
            trucks = trucks.Where(condition);
        }

        return await trucks.AsQueryable().ToListAsync();
    }

    public async Task<List<Transportation>> GetLastAsync(Expression<Func<Transportation, bool>> condition, int limit)
    {
        return await _context.Transportations
            .Include(item => item.Driver)
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrderStatusRecords)
            .Include(item => item.RoutePoints)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<Transportation>> GetFirstAsync(Expression<Func<Transportation, bool>> condition, int limit)
    {
        return await _context.Transportations
            .Include(item => item.Driver)
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrderStatusRecords)
            .Include(item => item.RoutePoints)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<Transportation> items)
    {
        await _context.Transportations.AddRangeAsync(items);
    }

    public Task UpdateAsync(Transportation item)
    {
        _context.Transportations.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<Transportation, bool>> condition)
    {
        IEnumerable<Transportation> removeItems = _context.Transportations.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}