using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories;

public class TransferAssignedTruckRepository: IRepository<AssignedTruckRecord>
{
    private readonly DatabaseContext _context;
    
    public TransferAssignedTruckRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<AssignedTruckRecord> GetAsync(Expression<Func<AssignedTruckRecord, bool>> condition)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<AssignedTruckRecord>> GetAllAsync()
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .ToListAsync();
    }
    
    public Task<List<AssignedTruckRecord>> GetAllAsync(List<Expression<Func<AssignedTruckRecord, bool>>> conditions)
    {
        return null;
    }

    public async Task<List<AssignedTruckRecord>> GetAllAsync(Expression<Func<AssignedTruckRecord, bool>> condition)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<AssignedTruckRecord>> GetLastAsync(Expression<Func<AssignedTruckRecord, bool>> condition, int limit)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<AssignedTruckRecord>> GetFirstAsync(Expression<Func<AssignedTruckRecord, bool>> condition, int limit)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<AssignedTruckRecord> items)
    {
        await _context.TransferAssignedDriverRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(AssignedTruckRecord item)
    {
        _context.TransferAssignedDriverRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<AssignedTruckRecord, bool>> condition)
    {
        IEnumerable<AssignedTruckRecord> removeItems = _context.TransferAssignedDriverRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}