using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.BaseImplementation.Repositories;

public class DriverRequestRepository : IRepository<DriverRequest>
{
    private readonly DatabaseContext _context;
    
    public DriverRequestRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<DriverRequest> GetAsync(Expression<Func<DriverRequest, bool>> condition)
    {
        return await _context.DriverRequests
            .Include(item => item.Driver)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<DriverRequest>> GetAllAsync()
    {
        return await _context.DriverRequests
            .Include(item => item.Driver)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }

    public async Task<List<DriverRequest>> GetAllAsync(Expression<Func<DriverRequest, bool>> condition)
    {
        return await _context.DriverRequests
            .Include(item => item.Driver)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }
    
    public async Task<List<DriverRequest>> GetAllAsync(List<Expression<Func<DriverRequest, bool>>> conditions)
    {
        IQueryable<DriverRequest> trucks = _context.DriverRequests
            .Include(item => item.Driver)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrder);
        
        foreach (var condition in conditions)
        {
            trucks = trucks.Where(condition);
        }

        return await trucks.AsQueryable().ToListAsync();
    }

    public async Task<List<DriverRequest>> GetLastAsync(Expression<Func<DriverRequest, bool>> condition, int limit)
    {
        return await _context.DriverRequests
            .Include(item => item.Driver)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<DriverRequest>> GetFirstAsync(Expression<Func<DriverRequest, bool>> condition, int limit)
    {
        return await _context.DriverRequests
            .Include(item => item.Driver)
            .Include(item => item.Truck)
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<DriverRequest> items)
    {
        await _context.DriverRequests.AddRangeAsync(items);
    }

    public Task UpdateAsync(DriverRequest item)
    {
        _context.DriverRequests.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<DriverRequest, bool>> condition)
    {
        IEnumerable<DriverRequest> removeItems = _context.DriverRequests.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}