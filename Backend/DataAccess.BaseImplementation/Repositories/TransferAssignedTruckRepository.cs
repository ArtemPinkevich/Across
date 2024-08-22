using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories;

public class TransferAssignedTruckRepository: IRepository<TransferAssignedTruckRecord>
{
    private readonly DatabaseContext _context;
    
    public TransferAssignedTruckRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransferAssignedTruckRecord> GetAsync(Expression<Func<TransferAssignedTruckRecord, bool>> condition)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransferAssignedTruckRecord>> GetAllAsync()
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .ToListAsync();
    }
    
    public Task<List<TransferAssignedTruckRecord>> GetAllAsync(List<Expression<Func<TransferAssignedTruckRecord, bool>>> conditions)
    {
        return null;
    }

    public async Task<List<TransferAssignedTruckRecord>> GetAllAsync(Expression<Func<TransferAssignedTruckRecord, bool>> condition)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferAssignedTruckRecord>> GetLastAsync(Expression<Func<TransferAssignedTruckRecord, bool>> condition, int limit)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferAssignedTruckRecord>> GetFirstAsync(Expression<Func<TransferAssignedTruckRecord, bool>> condition, int limit)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Include(item => item.Truck)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransferAssignedTruckRecord> items)
    {
        await _context.TransferAssignedDriverRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransferAssignedTruckRecord item)
    {
        _context.TransferAssignedDriverRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransferAssignedTruckRecord, bool>> condition)
    {
        IEnumerable<TransferAssignedTruckRecord> removeItems = _context.TransferAssignedDriverRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}